using System.Data.SqlClient;
using System.Text;
using ClinicTool.DataAccess.Dto;
using ClinicTool.DataAccess.Entities;
using ClinicTool.DataAccess.Repositories.Interfaces;

namespace ClinicTool.DataAccess.Repositories.Implementation
{
    public class AppointmentRepository : Repository, IAppointmentRepository
    {
        public AppointmentRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            this.sqlConnection = sqlConnection;
            this.sqlTransaction = sqlTransaction;
        }

        public List<Appointment> Search(AppointmentCriteriaDto appointmentCriteriaDto)
        {
            var sqlQuery = new StringBuilder(@"select
                        appointments.Id,
                        appointments.AppointmentDate,
                        appointments.Notes,
                        doctors.Id as DoctorId,
                        doctors.FullName as DoctorFullName,
                        patients.Id as PatientId,
                        patients.FullName as PatientFullName
                from Appointments appointments
                inner join Doctors doctors on doctors.Id = appointments.DoctorId
                inner join Patients patients on patients.Id = appointments.PatientId
                where appointments.IsHidden = 0");

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            if (appointmentCriteriaDto.DoctorId != null)
            {
                sqlQuery.Append(" and DoctorId = @DoctorId");
                sqlParameters.Add(new SqlParameter
                    { ParameterName = "@DoctorId", Value = appointmentCriteriaDto.DoctorId });
            }

            if (appointmentCriteriaDto.PatientId != null)
            {
                sqlQuery.Append(" and PatientId = @PatientId");
                sqlParameters.Add(new SqlParameter
                    { ParameterName = "@PatientId", Value = appointmentCriteriaDto.PatientId });
            }

            if (appointmentCriteriaDto.ValidAppointmentDateRange)
            {
                sqlQuery.Append(" and AppointmentDate between @StartAppointmentDate and @EndAppointmentDate");
                sqlParameters.Add(new SqlParameter
                    { ParameterName = "@StartAppointmentDate", Value = appointmentCriteriaDto.StartAppointmentDate });
                sqlParameters.Add(new SqlParameter
                    { ParameterName = "@EndAppointmentDate", Value = appointmentCriteriaDto.EndAppointmentDate });
            }

            if (string.IsNullOrEmpty(appointmentCriteriaDto.NotesFilter))
            {
                sqlQuery.Append(" and Notes @NotesFilter");
                sqlParameters.Add(new SqlParameter
                    { ParameterName = "@NotesFilter", Value = appointmentCriteriaDto.NotesFilter });
            }

            sqlQuery.Append("offset @Skip rows fetch next @Take rows only");

            var sqlCommand = CreateCommand(sqlQuery.ToString());

            sqlCommand.Parameters.AddRange(sqlParameters.ToArray());
            sqlCommand.Parameters.AddWithValue("@Skip", appointmentCriteriaDto.Skip);
            sqlCommand.Parameters.AddWithValue("@Take", appointmentCriteriaDto.Take);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var persons = new List<Appointment>();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    persons.Add(ConvertSqlDataToEntity(sqlDataReader));
                }
            }

            return persons;
        }

        public void Update(UpdateAppointmentDto updateAppointmentDto)
        {
            if (updateAppointmentDto.IsChanged)
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                var sqlQuery = new StringBuilder("update Appointments set");

                if (updateAppointmentDto.DoctorIsChanged)
                {
                    sqlQuery.Append("DoctorId = @DoctorId, ");
                    sqlParameters.Add(new SqlParameter
                        { ParameterName = "@DoctorId", Value = updateAppointmentDto.DoctorId });
                }

                if (updateAppointmentDto.PatientIsChanged)
                {
                    sqlQuery.Append("PatientId = @PatientId, ");
                    sqlParameters.Add(new SqlParameter
                        { ParameterName = "@PatientId", Value = updateAppointmentDto.PatientId });
                }

                if (updateAppointmentDto.AppointmentDateIsChanged)
                {
                    sqlQuery.Append("AppointmentDate = @AppointmentDate, ");
                    sqlParameters.Add(new SqlParameter
                        { ParameterName = "@AppointmentDate", Value = updateAppointmentDto.AppointmentDate });
                }

                if (updateAppointmentDto.NotesIsChanged)
                {
                    sqlQuery.Append("Notes = @Notes, ");
                    sqlParameters.Add(new SqlParameter
                        { ParameterName = "@Notes", Value = updateAppointmentDto.Notes });
                }

                sqlQuery.Append("where Id = @Id");

                var sqlCommand = CreateCommand(sqlQuery.ToString());

                sqlCommand.Parameters.AddRange(sqlParameters.ToArray());
                sqlCommand.Parameters.AddWithValue("Id", updateAppointmentDto.Id);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public Appointment Get(int id)
        {
            var sqlQuery = @"
                select
                        appointments.Id,
                        appointments.AppointmentDate,
                        appointments.Notes,
                        doctors.Id as DoctorId,
                        doctors.FullName as DoctorFullName,
                        patients.Id as PatientId,
                        patients.FullName as PatientFullName
                from Appointments appointments
                inner join Doctors doctors on doctors.Id = appointments.DoctorId
                inner join Patients patients on patients.Id = appointments.PatientId
                where appointments.Id = @Id";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var entity = new Appointment();
            if (sqlDataReader.Read())
            {
                entity = ConvertSqlDataToEntity(sqlDataReader);
            }

            return entity;
        }

        public void Add(Appointment entity)
        {
            var sqlQuery = "insert into Appointments(AppointmentDate, Notes, DoctorId, PatientId)" +
                           "output inserted.Id values(@AppointmentDate, @Notes, @DoctorId, @PatientId)";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("AppointmentDate", entity.AppointmentDate);
            sqlCommand.Parameters.AddWithValue("Notes", entity.Notes);
            sqlCommand.Parameters.AddWithValue("DoctorId", entity.Doctor.Id);
            sqlCommand.Parameters.AddWithValue("PatientId", entity.Patient.Id);

            entity.Id = (int)sqlCommand.ExecuteScalar();
        }

        public void Update(Appointment entity)
        {
            var sqlQuery =
                "update Appointments" +
                "set AppointmentDate = @AppointmentDate, Notes = @Notes, DoctorId = @DoctorId, PatientId = @PatientId" +
                "where Id = @Id";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", entity.Id);
            sqlCommand.Parameters.AddWithValue("AppointmentDate", entity.AppointmentDate);
            sqlCommand.Parameters.AddWithValue("Notes", entity.Notes);
            sqlCommand.Parameters.AddWithValue("DoctorId", entity.Doctor.Id);
            sqlCommand.Parameters.AddWithValue("PatientId", entity.Patient.Id);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            var sqlQuery = "delete from Appointments where Id = @Id";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        private Appointment ConvertSqlDataToEntity(SqlDataReader sqlDataReader)
        {
            return new Appointment
            {
                Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("Id")),
                AppointmentDate = sqlDataReader.GetFieldValue<DateTime>(sqlDataReader.GetOrdinal("AppointmentDate")),
                Notes = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("Notes")),
                Doctor = new Doctor
                {
                    Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("DoctorId")),
                    FullName = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("DoctorFullName"))
                },
                Patient = new Patient
                {
                    Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("PatientId")),
                    FullName = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("PatientFullName"))
                }
            };
        }
    }
}



using System.Data.SqlClient;
using ClinicTool.DataAccess.Entities;
using ClinicTool.DataAccess.Repositories.Interfaces;

namespace ClinicTool.DataAccess.Repositories.Implementation
{
    public class DoctorRepository : Repository, IDoctorRepository
    {
        public DoctorRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            this.sqlConnection = sqlConnection;
            this.sqlTransaction = sqlTransaction;
        }

        public Doctor Get(int id)
        {
            var sqlQuery = @"
                select doctors.Id, doctors.FullName, specialties.Id as SpecialtyId, specialties.Name as SpecialtyName
                from Doctors doctors
                inner join Specialties specialties on specialties.Id = doctors.SpecialtyId
                where doctors.Id = @Id";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var entity = new Doctor();
            if (sqlDataReader.Read())
            {
                entity = ConvertSqlDataToEntity(sqlDataReader);
            }

            return entity;
        }

        public void Add(Doctor entity)
        {
            var sqlQuery =
                "insert into Doctors(FullName, SpecialtyId) output inserted.Id values(@FullName, @SpecialtyId)";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("FullName", entity.FullName);
            sqlCommand.Parameters.AddWithValue("SpecialtyId", entity.Specialty.Id);

            entity.Id = (int)sqlCommand.ExecuteScalar();
        }

        public void Update(Doctor entity)
        {
            var sqlQuery = "update Doctors set FullName = @FullName, SpecialtyId = @SpecialtyId where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", entity.Id);
            sqlCommand.Parameters.AddWithValue("FullName", entity.FullName);
            sqlCommand.Parameters.AddWithValue("SpecialtyId", entity.Specialty.Id);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            var sqlQuery = "delete from Doctors where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        private Doctor ConvertSqlDataToEntity(SqlDataReader sqlDataReader)
        {
            return new Doctor
            {
                Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("Id")),
                FullName = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("FullName")),
                Specialty = new Specialty
                {
                    Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("SpecialtyId")),
                    Name = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("SpecialtyName"))
                }
            };
        }
    }
}





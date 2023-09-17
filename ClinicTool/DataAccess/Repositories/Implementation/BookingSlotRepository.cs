using System.Data.SqlClient;
using ClinicTool.DataAccess.Entities;
using ClinicTool.DataAccess.Repositories.Interfaces;

namespace ClinicTool.DataAccess.Repositories.Implementation
{
    public class BookingSlotRepository : Repository, IBookingSlotRepository
    {
        public BookingSlotRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            this.sqlConnection = sqlConnection;
            this.sqlTransaction = sqlTransaction;
        }

        public BookingSlot Get(int id)
        {
            var sqlQuery = @"
                select
                        bookingSlots.Id,
                        bookingSlots.StarDateTime,
                        bookingSlots.EndDateTime,
                        bookingSlots.Status,
                        doctors.Id as DoctorId,
                        doctors.FullName as DoctorFullName
                from BookingSlots bookingSlots
                inner join Doctors doctors on doctors.Id = bookingSlots.DoctorId
                where bookingSlots.Id = @Id";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var entity = new BookingSlot();
            if (sqlDataReader.Read())
            {
                entity = ConvertSqlDataToEntity(sqlDataReader);
            }

            return entity;
        }

        public void Add(BookingSlot entity)
        {
            var sqlQuery = "insert into BookingSlots(StarDate, EndDate, Status, DoctorId)" +
                           "output inserted.Id values(@StarDate, @EndDate, @Status, @DoctorId)";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("StarDate", entity.StarDate);
            sqlCommand.Parameters.AddWithValue("EndDate", entity.EndDate);
            sqlCommand.Parameters.AddWithValue("Status", entity.Status);
            sqlCommand.Parameters.AddWithValue("DoctorId", entity.Doctor.Id);

            entity.Id = (int)sqlCommand.ExecuteScalar();
        }

        public void Update(BookingSlot entity)
        {
            var sqlQuery =
                "update BookingSlots" +
                "set StarDate = @StarDate, EndDate = @EndDate, Status = @Status, DoctorId = @DoctorId" +
                "where Id = @Id";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", entity.Id);
            sqlCommand.Parameters.AddWithValue("StarDate", entity.StarDate);
            sqlCommand.Parameters.AddWithValue("EndDate", entity.EndDate);
            sqlCommand.Parameters.AddWithValue("Status", entity.Status);
            sqlCommand.Parameters.AddWithValue("DoctorId", entity.Doctor.Id);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            var sqlQuery = "delete from BookingSlots where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        private BookingSlot ConvertSqlDataToEntity(SqlDataReader sqlDataReader)
        {
            return new BookingSlot
            {
                Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("Id")),
                StarDate = sqlDataReader.GetFieldValue<DateTime>(sqlDataReader.GetOrdinal("StarDate")),
                EndDate = sqlDataReader.GetFieldValue<DateTime>(sqlDataReader.GetOrdinal("EndDate")),
                Status = sqlDataReader.GetFieldValue<BookingSlotStatus>(sqlDataReader.GetOrdinal("Status")),
                Doctor = new Doctor
                {
                    Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("DoctorId")),
                    FullName = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("DoctorFullName"))
                }
            };
        }
    }
}



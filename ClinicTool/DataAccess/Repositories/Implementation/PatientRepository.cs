using System.Data.SqlClient;
using ClinicTool.DataAccess.Entities;
using ClinicTool.DataAccess.Repositories.Interfaces;

namespace ClinicTool.DataAccess.Repositories.Implementation
{
    public class PatientRepository : Repository, IPatientRepository
    {
        public PatientRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            this.sqlConnection = sqlConnection;
            this.sqlTransaction = sqlTransaction;
        }

        public Patient Get(int id)
        {
            var sqlQuery = @"select Id, FullName, Phone, Email, BirthData from Patients where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var entity = new Patient();
            if (sqlDataReader.Read())
            {
                entity.Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("Id"));
                entity.FullName = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("FullName"));
                entity.Phone = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("Phone"));
                entity.Email = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("Email"));
                entity.BirthDate = sqlDataReader.GetFieldValue<DateTime>(sqlDataReader.GetOrdinal("BirthDateTime"));
            }

            return entity;
        }

        public void Add(Patient entity)
        {
            var sqlQuery =
                "insert into Patients(FullName, Phone, Email, BirthData)" +
                "output inserted.Id values(@FullName, @Phone, @Email, @BirthData)";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("FullName", entity.FullName);
            sqlCommand.Parameters.AddWithValue("Phone", entity.Phone);
            sqlCommand.Parameters.AddWithValue("Email", entity.Email);
            sqlCommand.Parameters.AddWithValue("BirthData", entity.BirthDate);

            entity.Id = (int)sqlCommand.ExecuteScalar();
        }

        public void Update(Patient entity)
        {
            var sqlQuery =
                "update Patients" +
                "set FullName = @FullName, Phone = @Phone, Email = @Email, BirthData = @BirthData" +
                "where Id = @Id";

            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", entity.Id);
            sqlCommand.Parameters.AddWithValue("FullName", entity.FullName);
            sqlCommand.Parameters.AddWithValue("Phone", entity.Phone);
            sqlCommand.Parameters.AddWithValue("Email", entity.Email);
            sqlCommand.Parameters.AddWithValue("BirthData", entity.BirthDate);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            var sqlQuery = "delete from Patients where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}






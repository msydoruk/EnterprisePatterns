using System.Data.SqlClient;
using ClinicTool.DataAccess.Entities;
using ClinicTool.DataAccess.Repositories.Interfaces;

namespace ClinicTool.DataAccess.Repositories.Implementation
{
    public class SpecialtyRepository : Repository, ISpecialtyRepository
    {
        public SpecialtyRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            this.sqlConnection = sqlConnection;
            this.sqlTransaction = sqlTransaction;
        }

        public Specialty Get(int id)
        {
            var sqlQuery = @"select Id, Name from Specialties where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var entity = new Specialty();
            if (sqlDataReader.Read())
            {
                entity.Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("Id"));
                entity.Name = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("Name"));
            }

            return entity;
        }

        public void Add(Specialty entity)
        {
            var sqlQuery = "insert into Specialties(Name) output inserted.Id values(@Name)";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Name", entity.Name);

            entity.Id = (int)sqlCommand.ExecuteScalar();
        }

        public void Update(Specialty entity)
        {
            var sqlQuery = "update Specialties set Name = @Name where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", entity.Id);
            sqlCommand.Parameters.AddWithValue("Name", entity.Name);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            var sqlQuery = "delete from Specialties where Id = @Id";
            var sqlCommand = CreateCommand(sqlQuery);

            sqlCommand.Parameters.AddWithValue("Id", id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}




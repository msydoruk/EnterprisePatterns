using System.Data.SqlClient;
using DataMapper.Configuration;
using DataMapper.Entities;

namespace DataMapper.DataMappers
{
    public class PersonDataMapper : IDataMapper<Person>
    {
        public List<Person> GetItems()
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConfigurationString());
            sqlConnection.Open();

            var sqlCommand = new SqlCommand(@"
                select persons.Id, persons.FirstName, persons.LastName, emails.Id as EmailId, emails.Mail
                from Persons persons
                inner join Emails emails on emails.Id = persons.EmailId", sqlConnection);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var persons = new List<Person>();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    persons.Add(ConvertSqlDataToEntity(sqlDataReader));
                }
            }

            return persons;
        }

        public Person GetById(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConfigurationString());
            sqlConnection.Open();

            var sqlCommand = new SqlCommand(@"
                select persons.Id, persons.FirstName, persons.LastName, emails.Id as EmailId, emails.Mail
                from Persons persons
                inner join Emails emails on emails.Id = persons.EmailId
                where persons.Id = @Id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", id);

            using var sqlDataReader = sqlCommand.ExecuteReader();

            var person = new Person();
            if (sqlDataReader.Read())
            {
                person = ConvertSqlDataToEntity(sqlDataReader);
            }

            return person;
        }

        public Person Add(Person entity)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConfigurationString());
            sqlConnection.Open();

            var sqlCommand =
                new SqlCommand(
                    "insert into Persons(FirstName, LastName, EmailId) output inserted.Id values(@FirstName, @LastName, @EmailId)");
            sqlCommand.Parameters.AddWithValue("FirstName", entity.FirstName);
            sqlCommand.Parameters.AddWithValue("LastName", entity.LastName);
            sqlCommand.Parameters.AddWithValue("EmailId", entity.Email.Id);

            entity.Id = (int)sqlCommand.ExecuteScalar();

            return entity;
        }

        public void Update(Person entity)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConfigurationString());
            sqlConnection.Open();

            var sqlCommand =
                new SqlCommand(
                    "update Persons set FirstName = @FirstName, LastName = @LastName, EmailId = @EmailId where Id = @Id");
            sqlCommand.Parameters.AddWithValue("Id", entity.Id);
            sqlCommand.Parameters.AddWithValue("FirstName", entity.FirstName);
            sqlCommand.Parameters.AddWithValue("LastName", entity.LastName);
            sqlCommand.Parameters.AddWithValue("EmailId", entity.Email.Id);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConfigurationString());
            sqlConnection.Open();

            var sqlCommand = new SqlCommand("delete from Persons where Id = @Id");
            sqlCommand.Parameters.AddWithValue("Id", id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        private Person ConvertSqlDataToEntity(SqlDataReader sqlDataReader)
        {
            return new Person
            {
                Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("Id")),
                FirstName = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("FirstName")),
                LastName = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("LastName")),
                Email = new Email
                {
                    Id = sqlDataReader.GetFieldValue<int>(sqlDataReader.GetOrdinal("EmailId")),
                    Name = sqlDataReader.GetFieldValue<string>(sqlDataReader.GetOrdinal("Mail"))
                }
            };
        }
    }
}

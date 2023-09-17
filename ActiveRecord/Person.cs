using System.Data;
using System.Data.SqlClient;

namespace ActiveRecord
{
    public class Person : IPerson
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public static List<Person> GetAll()
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlDataAdapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var sqlCommand = new SqlCommand("select Id, FirstName, FastName, Email from Persons", sqlConnection);

            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);

            var persons = new List<Person>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                persons.Add(new Person()
                {
                    Id = dataRow.Field<int>("Id"),
                    FirstName = dataRow.Field<string>("FirstName"),
                    LastName = dataRow.Field<string>("LastName"),
                    Email = dataRow.Field<string>("Email"),
                });
            }

            return persons;
        }

        public static Person GetById(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlDataAdapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var sqlCommand = new SqlCommand("select Id, FirstName, LastName, Email from Persons where Id = @Id",
                sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", id);

            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);

            return new Person()
            {
                Id = dataTable.Rows[0].Field<int>("Id"),
                FirstName = dataTable.Rows[0].Field<string>("FirstName"),
                LastName = dataTable.Rows[0].Field<string>("LastName"),
                Email = dataTable.Rows[0].Field<string>("Email"),
            };
        }

        public static Person GetByEmail(string email)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlDataAdapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var sqlCommand = new SqlCommand("select Id, FirstName, LastName, Email from Persons where Email = @Email",
                sqlConnection);
            sqlCommand.Parameters.AddWithValue("Email", email);

            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);

            return new Person()
            {
                Id = dataTable.Rows[0].Field<int>("Id"),
                FirstName = dataTable.Rows[0].Field<string>("FirstName"),
                LastName = dataTable.Rows[0].Field<string>("LastName"),
                Email = dataTable.Rows[0].Field<string>("Email"),
            };
        }

        public int Add()
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlCommand =
                new SqlCommand("insert into Persons(FirstName, LastName, Email) values(@FirstName, @LastName, @Email)");
            sqlCommand.Parameters.AddWithValue("FirstName", FirstName);
            sqlCommand.Parameters.AddWithValue("LastName", LastName);
            sqlCommand.Parameters.AddWithValue("Email", Email);

            return (int)sqlCommand.ExecuteScalar();
        }

        public void Update()
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlCommand =
                new SqlCommand(
                    "update Persons set FirstName = @FirstName, LastName = @LastName, Email = @Email where Id = @Id");
            sqlCommand.Parameters.AddWithValue("Id", Id);
            sqlCommand.Parameters.AddWithValue("FirstName", FirstName);
            sqlCommand.Parameters.AddWithValue("LastName", LastName);
            sqlCommand.Parameters.AddWithValue("Email", Email);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete()
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlCommand = new SqlCommand("delete from Persons where Id = @Id");
            sqlCommand.Parameters.AddWithValue("Id", Id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        public bool ValidateEmail()
        {
            throw new NotImplementedException();
        }

        public bool HasPermissions()
        {
            throw new NotImplementedException();
        }

        public void CorrectFirstName()
        {
            throw new NotImplementedException();
        }
    }
}

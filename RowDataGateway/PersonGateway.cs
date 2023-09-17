using System.Data;
using System.Data.SqlClient;

namespace RowDataGateway
{
    public class PersonGateway : IPersonGateway
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public static PersonGateway GetById(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlDataAdapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var sqlCommand = new SqlCommand("select Id, FirstName, LastName, Email from Person where Id = @Id",
                sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", id);

            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);

            return new PersonGateway
            {
                Id = id,
                FirstName = dataTable.Rows[0].Field<string>("FirstName"),
                LastName = dataTable.Rows[0].Field<string>("LastName"),
                Email = dataTable.Rows[0].Field<string>("Email")
            };
        }

        public int Add()
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlCommand =
                new SqlCommand("insert into Persons(FirstName, LastName, Email) values(@FirstName, @LastName, @Email)",
                    sqlConnection);
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
                    "update Persons set FirstName = @FirstName, LastName = @LastName, Email = @Email where Id = @Id",
                    sqlConnection);
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

            var sqlCommand =
                new SqlCommand("delete from Persons where Id = @Id",
                    sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", Id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}
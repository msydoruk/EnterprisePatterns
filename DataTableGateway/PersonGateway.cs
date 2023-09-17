using System.Data;
using System.Data.SqlClient;

namespace DataTableGateway
{
    public class PersonGateway : IPersonGateway
    {
        public DataTable GetAll()
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlDataAdapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var sqlCommand = new SqlCommand("select Id, FirstName, FastName, Email from Persons", sqlConnection);

            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        public DataRow GetById(int id)
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

            return dataTable.Rows[0];
        }

        public DataRow GetByEmail(string email)
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

            return dataTable.Rows[0];
        }

        public int Add(string firstName, string lastName, string email)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlCommand =
                new SqlCommand("insert into Persons(FirstName, LastName, Email) values(@FirstName, @LastName, @Email)");
            sqlCommand.Parameters.AddWithValue("FirstName", firstName);
            sqlCommand.Parameters.AddWithValue("LastName", lastName);
            sqlCommand.Parameters.AddWithValue("Email", email);

            return (int)sqlCommand.ExecuteScalar();
        }

        public void Update(int id, string firstName, string lastName, string email)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlCommand =
                new SqlCommand(
                    "update Persons set FirstName = @FirstName, LastName = @LastName, Email = @Email where Id = @Id");
            sqlCommand.Parameters.AddWithValue("Id", id);
            sqlCommand.Parameters.AddWithValue("FirstName", firstName);
            sqlCommand.Parameters.AddWithValue("LastName", lastName);
            sqlCommand.Parameters.AddWithValue("Email", email);

            sqlCommand.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionStringProvider.PrepareConnectionString());
            sqlConnection.Open();

            var sqlCommand = new SqlCommand("delete from Persons where Id = @Id");
            sqlCommand.Parameters.AddWithValue("Id", id);

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}

using System.Data.SqlClient;

namespace ClinicTool.DataAccess.Repositories.Implementation
{
    public abstract class Repository
    {
        protected SqlConnection sqlConnection;

        protected SqlTransaction sqlTransaction;

        protected SqlCommand CreateCommand(string sqlQuery)
        {
            return new SqlCommand(sqlQuery, sqlConnection, sqlTransaction);
        }
    }
}


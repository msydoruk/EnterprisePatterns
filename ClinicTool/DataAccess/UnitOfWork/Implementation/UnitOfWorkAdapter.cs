using System.Data.SqlClient;
using ClinicTool.DataAccess.UnitOfWork.Interfaces;

namespace ClinicTool.DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWorkAdapter : IUnitOfWorkAdapter
    {
        public SqlConnection sqlConnection;

        public SqlTransaction sqlTransaction;

        public IUnitOfWorkRepository? Repositories { get; set; }

        public UnitOfWorkAdapter(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            sqlTransaction = sqlConnection.BeginTransaction();

            Repositories = new UnitOfWorkRepository(sqlConnection, sqlTransaction);
        }

        public void Dispose()
        {
            sqlTransaction.Dispose();
            sqlConnection.Close();
            sqlConnection.Dispose();

            Repositories = null;
        }
        public void SaveChanges()
        {
            sqlTransaction.Commit();
        }
    }
}

using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;

namespace Retryer
{
    public class ConnectionPool : IConnectionPool
    {
        private readonly ConcurrentBag<IDbConnection> connections = new();
        private readonly string connectionString;
        private readonly IRetryerConnection retryerConnection;

        public ConnectionPool(string connectionString, IRetryerConnection retryerConnection)
        {
            this.connectionString = connectionString;
            this.retryerConnection = retryerConnection;
        }

        public IDbConnection Get()
        {
            if (connections.TryTake(out IDbConnection connection))
            {
                return connection;
            }

            var sqlConnection = new SqlConnection(connectionString);
            var isConnected = retryerConnection.Perform(new RetryableOpenSqlConnection(sqlConnection));

            if (!isConnected)
                throw new Exception();

            return new PooledConnection(sqlConnection);
        }

        public void Return(IDbConnection connection)
        {
            connections.Add(connection);
        }

        public void Clear()
        {
            foreach (var connection in connections)
            {
                connection.Close();
                connection.Dispose();
            }

            connections.Clear();
        }
    }
}

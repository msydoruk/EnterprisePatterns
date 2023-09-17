using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;

namespace ResourceTimer
{
    public class ConnectionPool : IConnectionPool
    {
        private readonly ConcurrentBag<IDbConnection> connections = new();
        private readonly string connectionString;

        public ConnectionPool(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Get()
        {
            if (connections.TryTake(out IDbConnection connection))
            {
                return connection;
            }

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return new TimedConnection(sqlConnection, this);
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

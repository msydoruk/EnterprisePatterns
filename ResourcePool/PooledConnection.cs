using System.Data;

namespace ResourcePool
{
    public class PooledConnection : IDbConnection
    {
        private readonly IDbConnection connection;

        public PooledConnection(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IDbTransaction BeginTransaction()
        {
            return connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return connection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
        }

        public IDbCommand CreateCommand()
        {
            return connection.CreateCommand();
        }

        public void Open()
        {
        }

        public string ConnectionString
        {
            get => connection.ConnectionString;
            set => connection.ConnectionString = value;
        }

        public int ConnectionTimeout => connection.ConnectionTimeout;
        
        public string Database => connection.Database;

        public ConnectionState State => connection.State;
    }
}

using System.Data;

namespace ResourceTimer
{
    public class TimedConnection : IDbConnection
    {
        private readonly IDbConnection connection;
        private readonly IConnectionPool connectionPool;
        private readonly ConnectionTimer connectionTimer;

        public TimedConnection(IDbConnection connection, IConnectionPool connectionPool)
        {
            this.connection = connection;
            this.connectionPool = connectionPool;
            connectionTimer = new ConnectionTimer(this);
            connectionTimer.Start();
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IDbTransaction BeginTransaction()
        {
            connectionTimer.Reset();
            return connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            connectionTimer.Reset();
            return connection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            connectionTimer.Reset();
            connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            connectionPool.Return(this);
        }

        public IDbCommand CreateCommand()
        {
            connectionTimer.Reset();
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

        public void ConnectionTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            connectionPool.Return(this);
        }
    }
}

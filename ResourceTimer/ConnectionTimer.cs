using Timer = System.Timers.Timer;

namespace ResourceTimer
{
    public class ConnectionTimer : IConnectionTimer
    {
        private const int ConnectionTimeoutMilliseconds = 3000;
        private readonly Timer connectionTimer = new(ConnectionTimeoutMilliseconds);

        public ConnectionTimer(TimedConnection connection)
        {
            connectionTimer.Elapsed += connection.ConnectionTimer_Elapsed;
            connectionTimer.AutoReset = false;
        }

        public void Start()
        {
            if (!connectionTimer.Enabled)
                connectionTimer.Start();
        }

        public void Stop()
        {
            if (connectionTimer.Enabled)
                connectionTimer.Stop();
        }

        public void Reset()
        {
            connectionTimer.Stop();
            connectionTimer.Start();
        }
    }
}

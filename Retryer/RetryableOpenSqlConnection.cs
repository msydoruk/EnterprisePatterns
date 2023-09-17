using System.Data;

namespace Retryer
{
    public class RetryableOpenSqlConnection : IRetryable
    {
        private readonly IDbConnection connection;

        public RetryableOpenSqlConnection(IDbConnection connection)
        {
            this.connection = connection;
        }

        public bool Attempt()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Recover()
        {
            throw new NotImplementedException();
        }
    }
}


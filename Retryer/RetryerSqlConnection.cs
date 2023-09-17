namespace Retryer
{
    public class RetryerSqlConnection : IRetryerConnection
    {
        private int maxRetryers = 10;

        public bool Perform(IRetryable retryable)
        {
            for (int i = 0; i < maxRetryers; i++)
            {
                if (retryable.Attempt())
                    return true;
                retryable.Recover();
            }

            return false;
        }
    }
}

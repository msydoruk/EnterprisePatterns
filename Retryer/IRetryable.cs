namespace Retryer
{
    public interface IRetryable
    {
        bool Attempt();

        void Recover();
    }
}

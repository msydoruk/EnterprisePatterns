using System.Data;

namespace Retryer
{
    public interface IConnectionPool
    {
        IDbConnection Get();

        void Return(IDbConnection connection);

        void Clear();
    }
}

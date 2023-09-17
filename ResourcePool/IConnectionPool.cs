using System.Data;

namespace ResourcePool
{
    public interface IConnectionPool
    {
        IDbConnection Get();

        void Return(IDbConnection connection);

        void Clear();
    }
}

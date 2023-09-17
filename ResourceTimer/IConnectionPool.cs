using System.Data;

namespace ResourceTimer
{
    public interface IConnectionPool
    {
        IDbConnection Get();

        void Return(IDbConnection connection);

        void Clear();
    }
}

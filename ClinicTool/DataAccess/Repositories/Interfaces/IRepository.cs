namespace ClinicTool.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T Get(int id);

        void Add(T entity);

        void Update(T entity);

        bool Delete(int id);
    }
}


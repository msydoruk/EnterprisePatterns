namespace DataMapper.DataMappers
{
    public interface IDataMapper<T>
    {
        List<T> GetItems();

        T GetById(int id);

        T Add(T entity);

        void Update(T entity);

        bool Delete(int id);
    }
}

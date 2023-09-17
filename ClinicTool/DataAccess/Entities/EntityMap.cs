using System.Collections.Concurrent;

namespace ClinicTool.DataAccess.Entities
{
    public class EntityMap<T> where T : Entity
    {
        private readonly Func<T> entityGenerator;
        private ConcurrentDictionary<int, T> entities = new();

        public T Get(int id)
        {
            return entities.GetOrAdd(id, entityGenerator());
        }

        public EntityMap(Func<T> entityGenerator)
        {
            this.entityGenerator = entityGenerator;
        }
    }
}

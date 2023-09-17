using System.Collections.Concurrent;

namespace CacheAccessor
{
    public class Database : IDatabase
    {
        private readonly List<string> _data = new();

        public Database()
        {
            throw new NotImplementedException();
        }

        public List<string> Select(string query)
        {
            return _data.Where(x => x.Contains(query)).ToList();
        }

        public void Insert(string item)
        {
            _data.Add(item);
        }
    }
}


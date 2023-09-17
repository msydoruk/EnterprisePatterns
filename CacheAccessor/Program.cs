using CacheAccessor;

var cacheAccessor = new CacheAccessor.CacheAccessor(new Cache(() => new Database()));

var items = cacheAccessor.Read("testConnectionString", "test");
Console.WriteLine(items);

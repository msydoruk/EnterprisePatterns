using CacheCollector;
using Microsoft.Extensions.Caching.Memory;


var memoryCache = new MemoryCache(new MemoryCacheOptions());

var customerRepository = new CustomerRepository();
var customerSettings = new CustomerSettings();
var cacheCollector = new CacheCollector.CacheCollector(memoryCache);
var customerAccessor = new CustomerAccessor(customerRepository, memoryCache, customerSettings, cacheCollector);

var customerService = new CustomerService(customerAccessor);

customerService.GetCustomers(1);
customerService.GetCustomers(1);
customerService.GetCustomers(2);

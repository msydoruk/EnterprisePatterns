using ResourcePool;

var connectionString = "Persist Security Info=False;Integrated Security=true;Initial Catalog=;server=(local)";

var connectionPool = new ConnectionPool(connectionString);
var connection1 = connectionPool.Get();
var connection2 = connectionPool.Get();
var connection3 = connectionPool.Get();
connectionPool.Return(connection1);
var connection4 = connectionPool.Get();
connectionPool.Clear();
var connection5 = connectionPool.Get();

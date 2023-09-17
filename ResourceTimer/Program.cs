using ResourceTimer;

var connectionString = "Persist Security Info=False;Integrated Security=true;Initial Catalog=;server=(local)";

var connectionPool = new ConnectionPool(connectionString);
var connection1 = connectionPool.Get();
var connection2 = connectionPool.Get();
var connection3 = connectionPool.Get();
var connection4 = connectionPool.Get();
var connection5 = connectionPool.Get();
var connection6 = connectionPool.Get();
var connection7 = connectionPool.Get();

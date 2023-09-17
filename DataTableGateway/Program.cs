using DataTableGateway;

var personGateway = new PersonGateway();

personGateway.Add("Jones", "Alexis", "jones@gmail.com");
personGateway.Update(1, "Jones", "Alexis", "jones.alexis@gmail.com");
personGateway.Delete(1);
var personListDataTable = personGateway.GetAll();
var personDataRow = personGateway.GetById(1);
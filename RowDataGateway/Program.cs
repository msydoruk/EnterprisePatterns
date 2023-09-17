using RowDataGateway;

var personGateway1 = PersonGateway.GetById(1);
personGateway1.FirstName = "Jones";
personGateway1.LastName = "Alexis";
personGateway1.Update();
personGateway1.Delete();

var personGateway2 = new PersonGateway
{
    FirstName = "Amy",
    LastName = "Chang",
    Email = "amy@gmail.com"
};
personGateway2.Add();
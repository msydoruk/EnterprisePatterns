using ActiveRecord;

var person = new Person
{
    FirstName = "Stephan",
    LastName = "John",
    Email = "stephan@gmail.com"
};

if (person.HasPermissions())
{
    person.CorrectFirstName();
    person.Add();
}

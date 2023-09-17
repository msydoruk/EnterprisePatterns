using DataMapper.DataMappers;
using DataMapper.Entities;

var personDataMapper = new PersonDataMapper();
var emailDataMapper = new EmailDataMapper();

var email = emailDataMapper.Add(new Email
{
    Name = "jonn@gmail.com"
});

var person = personDataMapper.GetById(1);
person.Email = email;
personDataMapper.Update(person);
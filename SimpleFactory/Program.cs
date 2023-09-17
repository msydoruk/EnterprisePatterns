using SimpleFactory;

var lastFirst = NameFactory.GetName("Chang,Amy");

Console.WriteLine(lastFirst.GetFirstName());
Console.WriteLine(lastFirst.GetLastName());

var firstFirst = NameFactory.GetName("Jones Alexis");

Console.WriteLine(firstFirst.GetFirstName());
Console.WriteLine(firstFirst.GetLastName());

var last = NameFactory.GetName("Jones");

Console.WriteLine(last.GetFirstName());
Console.WriteLine(last.GetLastName());
namespace SimpleFactory
{
    public abstract class Namer
    { 
        protected string firstName;

        protected string lastName;

        public string GetFirstName()
        {
            return firstName;
        }

        public string GetLastName()
        {
            return lastName;
        }
    }
}

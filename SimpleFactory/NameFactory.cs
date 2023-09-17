namespace SimpleFactory
{
    public  static class NameFactory
    {
        public static Namer GetName(string name)
        {
            if (name.IndexOf(@",", StringComparison.Ordinal) > 0)
            {
                return new LastFirst(name);
            }

            return new FirstFirst(name);
        }
    }
}


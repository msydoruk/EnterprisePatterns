﻿namespace SimpleFactory
{
    public class FirstFirst : Namer
    {
        public FirstFirst(string name)
        {
            int index = name.IndexOf(@" ", StringComparison.Ordinal);

            if (index > 0)
            {
                lastName = name.Substring(0, index);
                firstName = name.Substring(index + 1);
            }
            else
            {
                firstName = string.Empty;
                lastName = name;
            }
        }
    }
}


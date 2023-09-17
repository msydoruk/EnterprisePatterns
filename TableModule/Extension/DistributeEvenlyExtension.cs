using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule.Extension
{
    public static class DistributeEvenlyExtension
    {
        public static List<decimal> DistributeEvenly(decimal amount, int numDistributions)
        {
            var result = new List<decimal>();

            //Determines the whole number part of each distribution
            decimal baseValue = (int)(amount / numDistributions);

            //Fill the list with the baseValue, numDistributions times
            result.AddRange(Enumerable.Repeat(baseValue, numDistributions));

            //The remainder will be distributed in amounts of 0.5 until it runs out
            decimal remainder = amount - baseValue * numDistributions;

            int index = 0;

            while (remainder > 0)
            {
                result[index] += 0.5m;
                index++;
                if (index >= numDistributions)
                {
                    index = 0;
                }

                remainder -= 0.5m;
            }

            return result;
        }
    }
}

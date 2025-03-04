using System.Collections.Generic;

namespace homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int target = 30;
            int div = 2;
            bool added = false;
            List<int> elements = new List<int> { };
            while (target != 1)
            {    if (target % div != 0)
                {
                    added = false;
                    div++;
                    continue;
                }
                    target /= div;
                if (!added)
                {
                    added = true;
                    elements.Add(div);
                }
            }
            elements.ForEach(item => Console.WriteLine(item));

        }
    }
}

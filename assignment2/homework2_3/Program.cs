namespace homework2_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool[] Pri = new bool[99];
            for (int i = 0; i < Pri.Length; i++)
            {
                Pri[i] = true;
            }

            for (int i = 0;i < Pri.Length;i++)
            {
                if (!Pri[i]) { continue; }
                for (int j = i + 1; j < Pri.Length;j++)
                {
                    if (!Pri[j]) { continue; }
                    Pri[j] = (j + 2) % (i + 2) != 0;
                }
            }
            for(int i = 0;i< Pri.Length; i++) {
                if (Pri[i]) { Console.WriteLine(i + 2); }
            }
        }
    }
}

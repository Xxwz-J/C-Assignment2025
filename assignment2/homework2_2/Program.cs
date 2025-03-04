
namespace homework2._2
{

    internal class Program
    {
        static int Max(int[] list)
        {
            int max = int.MinValue;
            foreach (var item in list) { max = item > max ? item : max; }
            return max;
        }
        static int Min(int[] list)
        {
            int min = int.MaxValue;
            foreach (var item in list) { min = item < min ? item : min; }
            return min;
        }
        static int Average(int[] list)
        {
            int sum = 0;
            foreach (var item in list) { sum += item; }
            return sum / list.Length;
        }
        static int Sum(int[] list)
        {
            int sum = 0;
            foreach (var item in list) { sum += item; }
            return sum;
        }
        static void Main(string[] args)
        {
            int[] list = {2, 2, 33, 4, 11, 14, 15, 52, 2};
            Console.WriteLine(Max(list));
            Console.WriteLine(Min(list));
            Console.WriteLine(Average(list));
            Console.WriteLine(Sum(list));
        }
    }
}

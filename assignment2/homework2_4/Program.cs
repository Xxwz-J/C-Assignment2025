namespace homework2_4
{
    internal class Program
    {
        static bool Toplitz(int[][] matrix, int m,int n)
        {
            int size = m > n ? n : m;
            int elem = matrix[0][0];
            for (int i = 1; i < size; i++)
            {
                if (matrix[i][i] != elem)return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            int[][] myMatrix = new int[3][];
            myMatrix[0]=new int[3] { 1,2,3};
            myMatrix[1] = new int[3] { 2, 2, 3 };
            myMatrix[2] = new int[3] {3,2,1};
            Console.WriteLine(Toplitz(myMatrix, 3, 2));
            myMatrix[1] = new int[3] { 2, 1, 3 };
            Console.WriteLine(Toplitz(myMatrix, 3, 2));
        }
    }
}

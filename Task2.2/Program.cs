using System;

namespace Task2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, int, double[]> asyncMethod = new Func<double, int, double[]>(Pow);
            IAsyncResult asyncResult = asyncMethod.BeginInvoke(2, 10, CallBackMethod, "Async method ic completed!");
            double[] result = asyncMethod.EndInvoke(asyncResult);
            foreach (var item in result)
            {
                Console.Write($"{item}  ");
            }
        }
        static double[] Pow(double number, int degree)
        {
            double[] doubleArray = new double[degree];
            if (degree < 1)
                return null;

            for (int i = 1; i <= degree; i++)
            {
                doubleArray[i - 1] = Math.Pow(number, i);
            }
            return doubleArray;
        }
        static void CallBackMethod(IAsyncResult asyncResult)
        {
            if (asyncResult.IsCompleted)
                Console.WriteLine(asyncResult.AsyncState);
            else
                Console.WriteLine("Attention!");
        }
    }
}

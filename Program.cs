using System;
using System.Threading;
using Threads;

public class Demo
{
    private const string FILE_PATH = "D:\\TestFiles\\threads.txt"; // Replace to your own

    // function
    private static double func(double x)
    {
        // - ln|2 * sin(x/2)|
        return (double) -Math.Log(Math.Abs(2 * Math.Sin(x / 2)), Math.E);
    }

    // Taylor series
    private static double func(double x, int n)
    {
        // cos(n * x) / n
        // Ex. cos(x), cos(2x) / 2, cos(3x) / 3
        return (double) Math.Cos(n * x) / n;
    }

    static public void Main()
    {
        FileManager fileManager = new FileManager();
        fileManager.deleteFile(FILE_PATH);      // delete old file

        // Ex. "hello".PadRight(10) = "hello     "
        fileManager.writeLineInFile(FILE_PATH, "".PadRight(23) + "threads".PadRight(23) + "function".PadRight(23), true);

        Thread[] threads = new Thread[40];
        double[] results = new double[40];  // result of each thread

        double arg = -Math.PI / 5;      // [-pi / 5; 9 * pi / 5]
        while (arg <= 9 * Math.PI / 5)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                int index = i;
                threads[i] = new Thread(() =>       // lambda function
                {
                    results[index] = func(arg, index + 1);      // we pass the value to the thread and get the result
                });

                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                // Join waits for a specific thread to finish,
                // allowing the main thread to continue execution
                // only after the specified thread completes.
                threads[i].Join(); // This is not possible in the first for, otherwise we will wait for each thread
            }

            fileManager.writeLineInFile(FILE_PATH, arg.ToString().PadRight(23) +
                                                           results.Sum().ToString().PadRight(23) +
                                                           func(arg).ToString().PadRight(23), true);

            arg += 0.1; // next argument
        }
    }
}
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Threads;

public class Demo
{
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
        // Ex. cos(x), cos(2x) / 2, cos(3x) / 3 .... (n = 1, 2, 3 ....)
        return (double) Math.Cos(n * x) / n;
    }

    static public void Main()
    {
        Stopwatch timer = new Stopwatch();
        FileManager fileManager = new FileManager("D:\\TestFiles\\threads.txt");   // Replace to your own

        // Ex. "hello".PadRight(10) = "hello     "
        fileManager.writeLineInFile("".PadRight(23) + "threads".PadRight(23) + "function".PadRight(23));

        const int STEPS = 40;

        timer.Start();  // measure time

        Task[] threads = new Task[STEPS];
        double[] results = new double[STEPS];  // result of each thread

        double arg = -Math.PI / 5;      // [-pi/5; 9*pi/5]
        while (arg <= 9 * Math.PI / 5)
        {
            for (int i = 0; i < STEPS; i++)
            {
                int index = i;
                threads[index] = new Task(() =>       // lambda function
                {
                    results[index] = func(arg, index + 1);      // we pass the value to the thread and get the result
                });

                threads[index].Start();
            }

            for (int i = 0; i < STEPS; i++)
            {
                // Wait waits for a specific task to finish,
                // allowing the main thread to continue execution
                // only after the specified task completes.
                threads[i].Wait();  // This is not possible in the first for, otherwise we will wait for each task
            }

            fileManager.writeLineInFile(arg.ToString().PadRight(23) +
                                        results.Sum().ToString().PadRight(23) +
                                        func(arg).ToString().PadRight(23), true);

            arg += 0.1; // next argument
        }

        timer.Stop();
        Console.WriteLine("Program work:   " + timer.Elapsed.ToString());
    }
}
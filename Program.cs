using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static bool IsConverged(List<double> xOld, List<double> xNew, double epsilon)
    {
        for (int i = 0; i < xOld.Count; i++)
        {
            if (Math.Abs(xNew[i] - xOld[i]) > epsilon)
            {
                return false;
            }
        }
        return true;
    }

    static void Main()
    {
        // Coefficient matrix
        double[,] A = {
            { 5, 1, 1, 0 },
            { 1, 2, 0, 0 },
            { 1, 0, 4, 2 },
            { 0, 0, 2, 3 }
        };

        // Right-hand side vector
        double[] b = { 10, 5, 21, 18 };

        // Initial approximations
        List<double> x = new List<double> { 0, 0, 0, 0 };
        List<double> xOld = new List<double>(x);

        // Input accuracy
        Console.Write("Enter accuracy: ");
        double epsilon = double.Parse(Console.ReadLine());

        int iterations = 0;
        const int maxIterations = 1000; // Maximum number of iterations to avoid infinite loop

        do
        {
            xOld = new List<double>(x);
            Console.WriteLine($"Iteration {iterations + 1}:");
            for (int i = 0; i < A.GetLength(0); i++)
            {
                double sum = 0;
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        sum += A[i, j] * x[j];
                    }
                }
                x[i] = (b[i] - sum) / A[i, i];
                Console.WriteLine($"x[{i + 1}] = {x[i]}");
            }
            iterations++;
        } while (!IsConverged(xOld, x, epsilon) && iterations < maxIterations);

        // Check for convergence
        if (iterations >= maxIterations)
        {
            Console.WriteLine("The method did not converge within the given number of iterations.");
        }
        else
        {
            Console.WriteLine("Solution of the system:");
            for (int i = 0; i < x.Count; i++)
            {
                Console.WriteLine($"x[{i + 1}] = {x[i]}");
            }
            Console.WriteLine($"Number of iterations: {iterations}");
        }
    }
}

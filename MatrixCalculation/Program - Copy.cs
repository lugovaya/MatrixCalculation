using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrixSize = 12000;
            
            Console.WriteLine("Start to fill the input matrix");
            var inputMatrix = new ULongMatrix(matrixSize, matrixSize, true);
            inputMatrix.FillWithRandom(0, 255);
            Console.WriteLine("Result: complete");

            var watch = Stopwatch.StartNew();
            inputMatrix = AddStacks(inputMatrix);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds.ToString();

            Console.WriteLine("Time of method execution: {0} ms", elapsedMs);
            //Console.Write(inputMatrix.ToString());
            Console.ReadKey();
        }

        static ULongMatrix AddStacks(ULongMatrix matrix)
        {
            try
            {
                matrix.AddHStack();
                Console.WriteLine("Calculating of hstack is completed");

                matrix.AddVStack();
                Console.WriteLine("Calculating of vstack is completed");

                return matrix;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return matrix;
        }

        static UInt64[,] AddStacks(UInt64[,] matrix)
        {
            try
            {
                var count = matrix.Length;
                var rowCount = matrix.GetLength(0);
                int columnCount = matrix.GetLength(1);

                // add hstack
                for (var i = 0; i < rowCount - 1; i++)
                    for (var j = 0; j < columnCount - 1; j++)
                        matrix[i, columnCount - 1] += matrix[i, j];
                Console.WriteLine("Calculating of hstack is completed");

                // add vstack
                for (var j = 0; j < columnCount; j++)
                    for (var i = 0; i < rowCount - 1; i++)
                        matrix[rowCount - 1, j] += matrix[i, j];
                Console.WriteLine("Calculating of vstack is completed");

                return matrix;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return matrix;
        }


        static List<List<UInt64>> AddStacks(List<List<UInt64>> matrix)
        {
            try
            {
                // add hstack
                foreach (var row in matrix)
                    row.Add((UInt64)row
                            .Sum(x => Convert.ToInt64(x)));
                Console.WriteLine("Calculating of hstack is completed");

                var columnCount = matrix.Count + 1;

                var vstack = new List<UInt64>(columnCount);

                for (var i = 0; i < columnCount; i++)
                {
                    var vstackElement = 0ul;
                    foreach (var row in matrix)
                        vstackElement += row[i];

                    // fill vstack
                    vstack.Add(vstackElement);
                }
                Console.WriteLine("Calculating of vstack is completed");

                // add vstack
                matrix.Add(vstack);

                return matrix;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return matrix;
        }
        
        static void PrintResults(UInt64[,] matrix)
        {
            // square matrix
            var matrixSize = matrix.GetLength(0);

            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                    Console.Write("\t {0}", matrix[i, j]);
                Console.WriteLine();
            }
        }

        static string ToString<T>(List<T> list)
        {
            var res = list[0].ToString();

            for (var i = 1; i < list.Count; i++)
                res += string.Format("\t {0}", list[i].ToString());

            return res;
        }
    }
}

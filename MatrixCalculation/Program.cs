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
    }
}

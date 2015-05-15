using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalculation
{
    public interface IMatrix<T>
    {
        int Rows { get; set; }

        int Columns { get; set; }

        T[,] Array { get; set; }
        
        Boolean IsSquare();

        void FillWithRandom(int minimum, int maximum);

        void AddHStack();

        void AddVStack();

        void Resize(int rows, int cols);

        IMatrix<T> Transpose();
    }
}

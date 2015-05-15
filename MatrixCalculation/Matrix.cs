using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalculation
{
    public class Matrix<T> where T : IConvertible
    {
        public int _rows;
        public int _cols;
        public T[,] _array;

        public Matrix(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _array = new T[_rows, _cols];
        }

        public T this[int row, int col]
        {
            get { return _array[row, col]; }
            set { _array[row, col] = value; }
        }

        public Boolean IsSquare()
        {
            return (_rows == _cols);
        }

        public static Matrix<T> CreateRandom(int rows, int cols, int maximum)
        {
            return CreateRandom(rows, cols, 0, maximum);
        }

        public static Matrix<T> CreateRandom(int rows, int cols, int minimum, int maximum)
        {
            var matrix = new Matrix<T>(rows, cols);

            var ranGen = new Random();

            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                    matrix[i, j] = (T)Convert.ChangeType(ranGen.Next(minimum, maximum), typeof(T));

            return matrix;
        }

        public void AddHStack()
        {
            // add column
            _cols++;
            this.Resize(_rows, _cols);

            for (var i = 0; i < _rows; i++)
                for (var j = 0; j < _cols - 1; j++)
                    this[i, _cols - 1] = Add(this[i, _cols - 1], this[i, j]);
        }

        public void AddVStack()
        {
            // add row
            _rows++;
            this.Resize(_rows, _cols);

            for (var j = 0; j < _cols; j++)
                for (var i = 0; i < _rows - 1; i++)
                    this[_rows - 1, j] = Add(this[_rows - 1, j], this[i, j]);
        }

        public void Resize(int rows, int cols)
        {
            _array = ResizeArray(_array, rows, cols);
        }

        private T[,] ResizeArray(T[,] original, int rows, int cols)
        {
            var newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = original[i, j];
            return newArray;
        }

        private static T Add(T first, T second)
        {
            dynamic a = first;
            dynamic b = second;
            return a + b;
        }

        public static Matrix<T> Transpose(Matrix<T> m)
        {
            var t = new Matrix<T>(m._cols, m._rows);
            for (int i = 0; i < m._rows; i++)
                for (int j = 0; j < m._cols; j++)
                    t[j, i] = m[i, j];
            return t;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                    builder.Append(String.Format("\t{0}", this[i, j]));
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}

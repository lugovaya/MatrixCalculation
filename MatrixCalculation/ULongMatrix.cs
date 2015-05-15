using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalculation
{
    public class ULongMatrix : IMatrix<UInt64>
    {
        private bool _prepareToAddingStacks;

        public ULongMatrix(int rows, int cols, bool needStack = false)
        {
            Rows = rows;
            Columns = cols;
            Array = new UInt64[Rows, Columns];

            // add zero column and row or not
            // with _prepareToAddingStacks = true you get matrix like this:
            // 1 2 3 0
            // 4 5 6 0
            // 7 8 9 0
            // 0 0 0 0
            // сделано для увеличения производительности
            _prepareToAddingStacks = needStack;
        }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public ulong[,] Array { get; set; }
                
        public bool IsSquare()
        {
            return Rows == Columns;
        }

        public void FillWithRandom(int minimum, int maximum)
        {
            var ranGen = new Random();
            var rows = Rows;
            var cols = Columns;

            if (_prepareToAddingStacks)
            {
                rows--;
                cols--;
            }                

            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                    Array[i, j] = Convert.ToUInt64(ranGen.Next(minimum, maximum));
        }

        public void AddHStack()
        {
            if (!_prepareToAddingStacks)
            {
                // add column
                Columns++;
                this.Resize(Rows, Columns);
            }
            
            for (var i = 0; i < Rows; i++)
                for (var j = 0; j < Columns - 1; j++)
                    Array[i, Columns - 1] += Array[i, j];
        }

        public void AddVStack()
        {
            if (!_prepareToAddingStacks)
            {
                // add row
                Rows++;
                this.Resize(Rows, Columns);
            }
            
            for (var j = 0; j < Columns; j++)
                for (var i = 0; i < Rows - 1; i++)
                    Array[Rows - 1, j] += Array[i, j];
        }

        public void Resize(int rows, int cols)
        {
            Array = MatrixHelper.ResizeArray<UInt64>(Array, rows, cols);
        }

        public static ULongMatrix Transpose(ULongMatrix m)
        {
            var t = new ULongMatrix(m.Columns, m.Rows);
            for (int i = 0; i < m.Rows; i++)
                for (int j = 0; j < m.Columns; j++)
                    t.Array[j, i] = m.Array[i, j];
            return t;
        }

        public IMatrix<ulong> Transpose()
        {
            return Transpose(this);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    builder.Append(String.Format("\t{0}", Array[i, j]));
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}

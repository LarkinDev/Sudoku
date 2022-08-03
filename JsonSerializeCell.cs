using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class JsonSerializeCell
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Text { get; set; }

        public JsonSerializeCell()
        {

        }
        public JsonSerializeCell(SudokuCell cell)
        {
            Value = cell.Value;
            IsLocked = cell.IsLocked;
            X = cell.X;
            Y = cell.Y;
            Text = cell.Text;
        }
    }
}

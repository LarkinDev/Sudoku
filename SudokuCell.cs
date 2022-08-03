using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class SudokuCell:Button
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Clear()
        {
            this.Text = string.Empty;
            this.IsLocked = false;
        }
        public SudokuCell(JsonSerializeCell cell) 
        {
            this.X = cell.X;
            this.Y = cell.Y;
            this.Value = cell.Value;
            this.Text = cell.Text;
            this.IsLocked = cell.IsLocked;

            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.Size = new Size(55, 55);
            this.ForeColor = Color.Black; 
            this.Location = new Point(this.X * 55, this.Y * 55);
            this.BackColor = ((this.X / 3) + (this.Y / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Black;
        }
        public SudokuCell(int i, int j)
        {
            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.Size = new Size(55, 55);
            this.ForeColor = Color.Black;
            this.Location = new Point(i * 55, j * 55);
            this.BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Black;
            this.X = i;
            this.Y = j;
        }
    }
}

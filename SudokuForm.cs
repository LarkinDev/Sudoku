using System.Text.Json;
using System.Xml.Serialization;

namespace Sudoku
{
    public partial class SudokuForm : Form
    {
        public SudokuForm()
        {
            InitializeComponent();
            CreateSudokuCells();

            NewGame();
        }
        SudokuCell[,] cells = new SudokuCell[9, 9];
        Random random = new Random();

        private void CreateSudokuCells()
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    cells[i, j] = new SudokuCell(i, j);

                    cells[i, j].KeyPress += CellPressed;

                    sudokuPanel.Controls.Add(cells[i, j]);
                }
            }
        }
        private void CellPressed(object sender, KeyPressEventArgs e)
        {
            var cell = sender as SudokuCell;

            if (cell.IsLocked)
                return;

            int value;

            if (int.TryParse(e.KeyChar.ToString(), out value))
            {
                if (value == 0)
                    cell.Clear();
                else
                    cell.Text = value.ToString();

                cell.ForeColor = Color.Black;
            }
        }
        private void LoadValues()
        {
            foreach (var cell in cells)
            {
                cell.Value = 0;
                cell.Clear();
            }

            ValueForNextCell(0, -1);
        }
        private void NewGame()
        {
            LoadValues();

            ShowValueHints();
        }
        private void ShowValueHints()
        {
            for (int i = 0; i < 25; i++)
            {
                var rX = random.Next(9);
                var rY = random.Next(9);

                cells[rX, rY].Text = cells[rX, rY].Value.ToString();
                cells[rX, rY].ForeColor = Color.Black;
                cells[rX, rY].IsLocked = true;
            }
        }
        private bool ValueForNextCell(int i, int j)
        {
            if (++j > 8)
            {
                j = 0;
                if (++i > 8)
                    return true;
            }

            var value = 0;
            var nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            do
            {
                if (nums.Count == 0)
                {
                    cells[i, j].Value = 0;
                    return false;
                }
                value = nums[random.Next(0, nums.Count)];
                cells[i, j].Value = value;

                nums.Remove(value);

            }
            while (!CheckValue(value, i, j) || !ValueForNextCell(i, j));

            return true;
        }
        private bool CheckValue(int value, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i != y && cells[x, i].Value == value)
                    return false;

                if (i != x && cells[i, y].Value == value)
                    return false;
            }
            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                {
                    if (i != x && j != y && cells[i, j].Value == value)
                        return false;
                }
            }
            return true;
        }

        private void newGameBtn_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void checkBtn_Click(object sender, EventArgs e)
        {
            var wrongCells = new List<SudokuCell>();

            foreach (var cell in cells)
            {
                if (!string.Equals(cell.Value.ToString(), cell.Text))
                {
                    wrongCells.Add(cell);
                }
            }
            if (wrongCells.Any())
            {
                wrongCells.ForEach(x => x.ForeColor = Color.Red);
                MessageBox.Show("Wrong inputs");
            }
            else
            {
                MessageBox.Show("You Wins");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.ShowDialog();
            if(this.saveFileDialog.FileName!="")
            {
                foreach (var cell in cells)
                {
                    var s = JsonSerializer.Serialize(new JsonSerializeCell(cell));
                    using (StreamWriter sw = File.AppendText(saveFileDialog.FileName))
                    {
                        sw.WriteLine(s);
                    }

                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
            if (this.openFileDialog.FileName != "")
            {
                    var strs = File.ReadAllLines(this.openFileDialog.FileName);
                    sudokuPanel.Controls.Clear();
                    foreach (var str in strs)
                    {
                        var jcell = JsonSerializer.Deserialize<JsonSerializeCell>(str);
                        if (jcell != null)
                        {
                            cells[jcell.X, jcell.Y] = new SudokuCell(jcell);

                            cells[jcell.X, jcell.Y].KeyPress += CellPressed;

                            sudokuPanel.Controls.Add(cells[jcell.X, jcell.Y]);
                        }
                    }
                }               
        }
    }
}
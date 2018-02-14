using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    class Solver
    {
        SudokuContents content;
        List<int>[,] last;

        Panel p;

        public Solver(Panel panel)
        {
            p = panel;
            var knownValues = new int[9, 9];
            int sudokuIndex = 2;
            // x,y
            if (sudokuIndex == 0)
            {
                knownValues[0, 0] = 7;
                knownValues[3, 0] = 6;
                knownValues[8, 0] = 2;
                knownValues[1, 1] = 3;
                knownValues[3, 1] = 2;
                knownValues[7, 1] = 7;
                knownValues[2, 2] = 8;
                knownValues[3, 2] = 1;
                knownValues[4, 2] = 7;
                knownValues[6, 2] = 4;
                knownValues[6, 3] = 8;
                knownValues[7, 3] = 4;
                knownValues[8, 3] = 6;
                knownValues[2, 4] = 3;
                knownValues[4, 4] = 1;
                knownValues[6, 4] = 9;
                knownValues[0, 5] = 5;
                knownValues[1, 5] = 4;
                knownValues[2, 5] = 2;
                knownValues[2, 6] = 5;
                knownValues[4, 6] = 3;
                knownValues[5, 6] = 9;
                knownValues[6, 6] = 2;
                knownValues[1, 7] = 7;
                knownValues[5, 7] = 4;
                knownValues[7, 7] = 9;
                knownValues[0, 8] = 2;
                knownValues[5, 8] = 1;
                knownValues[8, 8] = 7;
            }
            else if (sudokuIndex == 1)
            {
                knownValues[0, 1] = 3;
                knownValues[0, 8] = 1;
                knownValues[1, 3] = 1;
                knownValues[1, 5] = 2;
                knownValues[1, 7] = 9;
                knownValues[2, 2] = 5;
                knownValues[2, 3] = 6;
                knownValues[2, 5] = 9;
                knownValues[3, 0] = 1;
                knownValues[3, 1] = 5;
                knownValues[3, 4] = 7;
                knownValues[3, 6] = 2;
                knownValues[4, 2] = 7;
                knownValues[4, 6] = 4;
                knownValues[5, 2] = 6;
                knownValues[5, 4] = 9;
                knownValues[5, 7] = 8;
                knownValues[5, 8] = 3;
                knownValues[6, 3] = 7;
                knownValues[6, 5] = 4;
                knownValues[6, 6] = 9;
                knownValues[7, 1] = 4;
                knownValues[7, 3] = 9;
                knownValues[7, 5] = 8;
                knownValues[8, 0] = 6;
                knownValues[8, 7] = 4;
            }
            else if (sudokuIndex == 2)
            {
                knownValues[2, 0] = 2;
                knownValues[6, 0] = 1;
                knownValues[2, 1] = 5;
                knownValues[3, 1] = 7;
                knownValues[4, 1] = 2;
                knownValues[5, 1] = 1;
                knownValues[6, 1] = 8;
                knownValues[0, 2] = 3;
                knownValues[8, 2] = 6;
                knownValues[4, 3] = 5;
                knownValues[0, 4] = 7;
                knownValues[8, 4] = 2;
                knownValues[1, 5] = 3;
                knownValues[3, 5] = 4;
                knownValues[5, 5] = 6;
                knownValues[7, 5] = 8;
                knownValues[0, 6] = 6;
                knownValues[3, 6] = 2;
                knownValues[5, 6] = 9;
                knownValues[8, 6] = 5;
                knownValues[2, 7] = 9;
                knownValues[6, 7] = 6;
                knownValues[0, 8] = 1;
                knownValues[3, 8] = 5;
                knownValues[5, 8] = 4;
                knownValues[8, 8] = 7;
            }
            else
            {
                throw new InvalidOperationException("Invalid sudoku index");
            }
            content = new SudokuContents(knownValues);
            UpdateDrawing();
        }

        internal void Start()
        {
            SolveCycleMain(); // run on thread
        }

        void SolveCycleMain()
        {
            UpdateDrawing();
            while (content.RecheckValues()) // run until done
            {
                Thread.Sleep(500);
                UpdateDrawing();
            }
            UpdateDrawing();
        }

        const int size = 60;
        private void UpdateDrawing()
        {
            p.Invoke((MethodInvoker)delegate
            {
                p.Controls.Clear();
                for (int px = 0; px < 9; px++)
                {
                    for (int py = 0; py < 9; py++)
                    {
                        var l = new Label
                        {
                            Size = new Size((int)(size), 12),
                            Text = (this.content?.Data[px, py].GetShort(true) ?? "?").ToString(),
                            ForeColor = (this.content?.Data[px, py].IsDirty != false) ? Color.Red : Color.Black,
                            Location = new Point((px + px / 3) * size, (py + py / 3) * size)
                        };
                        l.Show();
                        p.Controls.Add(l);
                        this.content?.Data[px, py].MarkClean();
                    }
                }
                p.Update();
            });
        }
    }
}

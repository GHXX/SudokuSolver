using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
            this.p = panel;
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
                knownValues[0, 0] = 5;
                knownValues[1, 0] = 2;
                knownValues[6, 1] = 4;
                knownValues[7, 1] = 9;
                knownValues[8, 1] = 6;
                knownValues[3, 2] = 3;
                knownValues[4, 2] = 4;
                knownValues[5, 2] = 8;
                knownValues[1, 3] = 3;
                knownValues[5, 3] = 9;
                knownValues[8, 3] = 7;
                knownValues[2, 4] = 2;
                knownValues[3, 4] = 4;
                knownValues[4, 4] = 5;
                knownValues[5, 4] = 1;
                knownValues[8, 4] = 3;
                knownValues[0, 5] = 4;
                knownValues[1, 5] = 5;
                knownValues[2, 5] = 6;
                knownValues[4, 5] = 8;
                knownValues[6, 6] = 2;
                knownValues[7, 6] = 6;
                knownValues[0, 7] = 9;
                knownValues[2, 7] = 1;
                knownValues[2, 8] = 5;
                knownValues[3, 8] = 9;
                knownValues[4, 8] = 7;
                knownValues[5, 8] = 4;
            }
            else if (sudokuIndex == 3)
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
            else if (sudokuIndex == 4)
            {
                knownValues[2, 0] = 8;
                knownValues[4, 0] = 5;
                knownValues[7, 0] = 4;
                knownValues[1, 1] = 5;
                knownValues[2, 1] = 1;
                knownValues[3, 1] = 4;
                knownValues[4, 1] = 8;
                knownValues[8, 1] = 2;
                knownValues[2, 2] = 4;
                knownValues[4, 2] = 6;
                knownValues[5, 2] = 2;
                knownValues[6, 3] = 8;
                knownValues[2, 4] = 5;
                knownValues[6, 4] = 1;
                knownValues[7, 4] = 3;
                knownValues[8, 4] = 7;
                knownValues[0, 5] = 1;
                knownValues[1, 5] = 4;
                knownValues[7, 5] = 9;
                knownValues[4, 6] = 3;
                knownValues[6, 6] = 4;
                knownValues[7, 6] = 7;
                knownValues[8, 6] = 1;
                knownValues[3, 7] = 6;
                knownValues[7, 7] = 5;
                knownValues[3, 8] = 2;
            }
            else
            {
                throw new InvalidOperationException("Invalid sudoku index");
            }
            this.content = new SudokuContents(knownValues);
            UpdateDrawing();
        }

        internal void Load(int[,] data)
        {
            this.content = new SudokuContents(data);
            UpdateDrawing();
        }

        internal void Start()
        {
            SolveCycleMain(); // run on thread
        }

        [Obsolete]
        void SolveCycleMain()
        {
            Solve();
            return;
            UpdateDrawing();
            while (this.content.RecheckValues()) // run until done
            {
                Thread.Sleep(500);
                UpdateDrawing();
            }
            UpdateDrawing();
        }

        void Solve()
        {
            while (this.content.RecheckValues()) { }    // update sudoku

            if (!this.IsSolved && this.IsValid)
            {
                var oldContent = new SudokuContents(this.content);
                int maxVals = 2;
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        var data = this.content.Data[x, y];
                        if (!data.IsFinished)
                        {
                            var pVals = data.PossibleValues;
                            if (pVals.Count == maxVals)
                            {
                                for (int i = 0; i < pVals.Count; i++)
                                {
                                    var newContent = new SudokuContents(oldContent);
                                    newContent.Data[x, y].SetNumber(pVals[i]);
                                    this.content = newContent;
                                    while (this.content.RecheckValues())
                                    {
                                        Thread.Sleep(500);
                                        UpdateDrawing();
                                    }
                                    if (this.content.IsSolved())
                                    {
                                        UpdateDrawing();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                maxVals++;
                //var subSolver = new Solver(null);
                //var newContent = new SudokuContents(this.content);
                //subSolver.Solve();
                //if (subSolver.IsValid)
                //{
                //    this.content = subSolver.content;
                //}
            }
        }

        bool IsSolved => this.content.IsSolved();
        bool IsValid => this.content.IsValid();

        const int size = 40;
        private void UpdateDrawing()
        {
            if (this.p != null)
            {
                bool valid = this.content.IsValid();
                this.p.Invoke((MethodInvoker)delegate
                {
                    this.p.Controls.Clear();
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
                            this.p.Controls.Add(l);
                            this.content?.Data[px, py].MarkClean();
                        }
                    }
                    var lblControl = this.p.Parent.Controls.Find("lblValid", false).First();
                    lblControl.Text = "Valid: " + (valid ? "True" : "False");
                    lblControl.ForeColor = valid ? Color.Green : Color.Red;
                    this.p.Update();
                });
            }
        }
    }
}

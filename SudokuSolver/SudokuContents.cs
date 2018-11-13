using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    internal class SudokuContents
    {
        SudokuNumberStack[,] data;
        readonly int[] Arr1to9 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public SudokuNumberStack[,] Data => this.data;
        int iteration = 0;
        int[] debug = { -1, -1 }; // -1 = disabled

        public SudokuNumberStack[] GetLine(int index)
        {
            var ret = new List<SudokuNumberStack>();
            for (int i = 0; i < 9; i++)
            {
                ret.Add(this.data.GetValue(i, index) as SudokuNumberStack);
            }
            return ret.ToArray();
        }

        public SudokuNumberStack[] GetColumn(int index)
        {
            var ret = new List<SudokuNumberStack>();
            for (int i = 0; i < 9; i++)
            {
                ret.Add(this.data.GetValue(index, i) as SudokuNumberStack);
            }
            return ret.ToArray();
        }

        public SudokuNumberStack[] GetSquare(int index) // 0 - 8
        {
            var ret = new List<SudokuNumberStack>();
            for (int i = 0; i < 9; i++)
            {
                ret.Add(this.data.GetValue((index % 3) * 3 + i % 3, (index / 3) * 3 + i / 3) as SudokuNumberStack);
            }
            return ret.ToArray();
        }
        public bool IsSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!this.data[i, j].IsFinished)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsValid()
        {
            for (int i = 0; i < 9; i++)
            {
                var ln = GetLine(i).Where(x => x.IsFinished);
                if (ln.Distinct().Count() != ln.Count())
                {
                    return false;
                }

                var col = GetColumn(i).Where(x => x.IsFinished);
                if (col.Distinct().Count() != col.Count())
                {
                    return false;
                }

                var sq = GetSquare(i).Where(x => x.IsFinished);
                if (sq.Distinct().Count() != sq.Count())
                {
                    return false;
                }
            }
            return true;
        }

        public SudokuContents(int[,] knownValues)
        {
            this.data = new SudokuNumberStack[9, 9];
            for (int px = 0; px < 9; px++)
            {
                for (int py = 0; py < 9; py++)
                {
                    this.data[px, py] = new SudokuNumberStack();
                    if (knownValues[px, py] != 0)
                    {
                        this.data[px, py].SetNumber(knownValues[px, py]);
                    }
                }
            }
        }

        public SudokuContents(SudokuContents content)
        {
            this.data = content.Data;
        }

        public bool RecheckValues()
        {
            this.iteration++;
            bool checkChanged = false;

            if (checkChanged) // recalc
                return true;

            // recheck columns
            for (int py = 0; py < 9; py++)
            {
                for (int px = 0; px < 9; px++) // columns
                {
                    if (!this.data[px, py].IsFinished)
                    {
                        var number = this.data[px, py];
                        var ColumnVals = new List<int>();

                        for (int py2 = 0; py2 < 9; py2++)
                        {
                            if (this.data[px, py2].IsFinished)
                                ColumnVals.Add(this.data[px, py2].PossibleValues[0]);
                        }


                        foreach (var num in this.data[px, py].PossibleValues.Where(x => ColumnVals.Contains(x)).ToList())
                        {
                            this.data[px, py].EliminateNumber(num);
                            checkChanged = true;
                        }
                    }
                }

                // count finished in line
                var finished = new List<int>();
                for (int px = 0; px < 9; px++)
                {
                    if (this.data[px, py].IsFinished)
                        finished.Add(this.data[px, py].PossibleValues[0]);
                }
                if (finished.Count == 8)
                {
                    for (int px = 0; px < 9; px++)
                    {
                        if (!this.data[px, py].IsFinished)
                        {
                            this.data[px, py].SetNumber(this.Arr1to9.Except(finished).First());
                            this.data[px, py].SetDirty();
                            checkChanged = true;
                            break;
                        }
                    }
                }
            }

            if (checkChanged)
                return true;


            // recheck lines
            for (int px = 0; px < 9; px++)
            {
                // eliminate values
                for (int py = 0; py < 9; py++) // lines
                {
                    if (!this.data[px, py].IsFinished) // if unknown
                    {
                        var number = this.data[px, py];
                        var ColumnVals = new List<int>();

                        for (int px2 = 0; px2 < 9; px2++)
                        {
                            if (this.data[px2, py].IsFinished)
                                ColumnVals.Add(this.data[px2, py].PossibleValues[0]);
                        }


                        foreach (var num in this.data[px, py].PossibleValues.Where(x => ColumnVals.Contains(x)).ToList())
                        {
                            this.data[px, py].EliminateNumber(num);
                            checkChanged = true;
                        }
                    }
                }

                // count finished in column
                var finished = new List<int>();
                for (int py = 0; py < 9; py++)
                {
                    if (this.data[px, py].IsFinished)
                        finished.Add(this.data[px, py].PossibleValues[0]);
                }
                if (finished.Count == 8)
                {
                    for (int py = 0; py < 9; py++)
                    {
                        if (!this.data[px, py].IsFinished)
                        {
                            this.data[px, py].SetNumber(this.Arr1to9.Except(finished).First());
                            this.data[px, py].SetDirty();
                            checkChanged = true;
                            break;
                        }
                    }
                }
            }

            // recheck 3x3 squares
            for (int square = 0; square < 9; square++)
            {
                int offsetX = (square % 3) * 3;
                int offsetY = (square / 3) * 3;

                var finished = new List<int>();

                for (int line = 0; line < 3; line++) // lines
                {
                    for (int column = 0; column < 3; column++) // columns
                    {
                        var number = this.data[offsetX + column, offsetY + line];
                        if (number.IsFinished)
                            finished.Add(number.PossibleValues[0]);
                    }
                }

                // eliminate numbers that are filled in already
                for (int py = 0; py < 3; py++) // lines
                {
                    for (int px = 0; px < 3; px++) // columns
                    {
                        foreach (var item in finished)
                        {
                            if (!this.data[offsetX + px, offsetY + py].IsFinished && this.data[offsetX + px, offsetY + py].PossibleValues.Contains(item))
                            {
                                this.data[offsetX + px, offsetY + py].EliminateNumber(item);
                                checkChanged = true;
                            }
                        }
                    }
                }
                // ----------------------------


                // finish 8 out of 9 squares

                if (finished.Count == 8)
                {
                    for (int py = 0; py < 3; py++) // lines
                    {
                        for (int px = 0; px < 3; px++) // columns
                        {
                            var number = this.data[offsetX + px, offsetY + py];
                            if (!this.data[offsetX + px, offsetY + py].IsFinished)
                            {
                                this.data[offsetX + px, offsetY + py].SetNumber(this.Arr1to9.Except(finished).First());
                                this.data[offsetX + px, offsetY + py].SetDirty();
                                checkChanged = true;
                                break;
                            }
                        }
                    }
                }
                else // set number if nowhere else possible
                {
                    var slotValues = new List<SudokuNumberStack>();
                    for (int py = 0; py < 3; py++) // lines
                    {
                        for (int px = 0; px < 3; px++) // columns
                        {
                            slotValues.Add(this.data[offsetX + px, offsetY + py]);
                        }
                    }

                    var setWherePossible = new List<int>();
                    for (int n = 1; n <= 9; n++)
                    {
                        if (!slotValues.Any(y => y.IsFinished && y.PossibleValues[0] == n) && slotValues.Count(x => x.PossibleValues.Contains(n)) == 1)
                        {
                            setWherePossible.Add(n);
                        }
                    }

                    foreach (var item in setWherePossible)
                    {
                        for (int py = 0; py < 3; py++) // lines
                        {
                            for (int px = 0; px < 3; px++) // columns
                            {
                                if (this.data[offsetX + px, offsetY + py].PossibleValues.Contains(item))
                                {
                                    this.data[offsetX + px, offsetY + py].SetNumber(item);
                                    checkChanged = true;
                                }
                            }
                        }
                    }

                }
            }

            return checkChanged;
        }
    }
}
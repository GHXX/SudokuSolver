using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    internal class SudokuContents
    {
        SudokuNumberStack[,] data;
        readonly int[] Arr1to9 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public SudokuNumberStack[,] Data => data;
        int iteration = 0;
        int[] debug = { -1, -1 }; // -1 = disabled

        public SudokuContents(int[,] knownValues)
        {
            data = new SudokuNumberStack[9, 9];
            for (int px = 0; px < 9; px++)
            {
                for (int py = 0; py < 9; py++)
                {
                    data[px, py] = new SudokuNumberStack();
                    if (knownValues[px, py] != 0)
                    {
                        data[px, py].SetNumber(knownValues[px, py]);
                    }
                }
            }
        }

        public bool RecheckValues()
        {
            iteration++;
            bool checkChanged = false;

            if (checkChanged) // recalc
                return true;

            // recheck columns
            for (int py = 0; py < 9; py++)
            {
                for (int px = 0; px < 9; px++) // columns
                {
                    if (!data[px, py].IsFinished)
                    {
                        var number = data[px, py];
                        var ColumnVals = new List<int>();

                        for (int py2 = 0; py2 < 9; py2++)
                        {
                            if (data[px, py2].IsFinished)
                                ColumnVals.Add(data[px, py2].GetPossibleValues[0]);
                        }


                        foreach (var num in data[px, py].GetPossibleValues.Where(x => ColumnVals.Contains(x)).ToList())
                        {
                            data[px, py].EliminateNumber(num);
                            checkChanged = true;
                        }
                    }
                }

                // count finished in line
                var finished = new List<int>();
                for (int px = 0; px < 9; px++)
                {
                    if (data[px, py].IsFinished)
                        finished.Add(data[px, py].GetPossibleValues[0]);
                }
                if (finished.Count == 8)
                {
                    for (int px = 0; px < 9; px++)
                    {
                        if (!data[px, py].IsFinished)
                        {
                            data[px, py].SetNumber(Arr1to9.Except(finished).First());
                            data[px, py].SetDirty();
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
                    if (!data[px, py].IsFinished) // if unknown
                    {
                        var number = data[px, py];
                        var ColumnVals = new List<int>();

                        for (int px2 = 0; px2 < 9; px2++)
                        {
                            if (data[px2, py].IsFinished)
                                ColumnVals.Add(data[px2, py].GetPossibleValues[0]);
                        }


                        foreach (var num in data[px, py].GetPossibleValues.Where(x => ColumnVals.Contains(x)).ToList())
                        {
                            data[px, py].EliminateNumber(num);
                            checkChanged = true;
                        }
                    }
                }

                // count finished in column
                var finished = new List<int>();
                for (int py = 0; py < 9; py++)
                {
                    if (data[px, py].IsFinished)
                        finished.Add(data[px, py].GetPossibleValues[0]);
                }
                if (finished.Count == 8)
                {
                    for (int py = 0; py < 9; py++)
                    {
                        if (!data[px, py].IsFinished)
                        {
                            data[px, py].SetNumber(Arr1to9.Except(finished).First());
                            data[px, py].SetDirty();
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
                        var number = data[offsetX + column, offsetY + line];
                        if (number.IsFinished)
                            finished.Add(number.GetPossibleValues[0]);
                    }
                }

                // eliminate numbers that are filled in already
                for (int py = 0; py < 3; py++) // lines
                {
                    for (int px = 0; px < 3; px++) // columns
                    {
                        foreach (var item in finished)
                        {
                            if (!data[offsetX + px, offsetY + py].IsFinished && data[offsetX + px, offsetY + py].GetPossibleValues.Contains(item))
                            {
                                data[offsetX + px, offsetY + py].EliminateNumber(item);
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
                            var number = data[offsetX + px, offsetY + py];
                            if (!data[offsetX + px, offsetY + py].IsFinished)
                            {
                                data[offsetX + px, offsetY + py].SetNumber(Arr1to9.Except(finished).First());
                                data[offsetX + px, offsetY + py].SetDirty();
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
                            slotValues.Add(data[offsetX + px, offsetY + py]);
                        }
                    }

                    var setWherePossible = new List<int>();
                    for (int n = 1; n <= 9; n++)
                    {
                        if (!slotValues.Any(y => y.IsFinished && y.GetPossibleValues[0] == n) && slotValues.Count(x => x.GetPossibleValues.Contains(n)) == 1)
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
                                if (data[offsetX + px, offsetY + py].GetPossibleValues.Contains(item))
                                {
                                    data[offsetX + px, offsetY + py].SetNumber(item);
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
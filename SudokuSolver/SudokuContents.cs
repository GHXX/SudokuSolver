using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    internal class SudokuContents
    {
        SudokuNumberStack[,] data;
        readonly int[] Arr1to9 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public SudokuContents(int[,] knownValues)
        {
            data = new SudokuNumberStack[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    data[i, j] = new SudokuNumberStack();
                    if (knownValues[i,j] != 0)
                    {
                        data[i, j].SetNumber(knownValues[i, j]);
                    }
                }
            }
        }

        public bool RecheckValues()
        {
            bool checkChanged = false;

            if (checkChanged) // recalc
                return true;

            // recheck lines
            for (int i = 0; i < 9; i++)
            {
                // eliminate values
                for (int k = 0; k < 9; k++) // columns
                {
                    if (!data[i, k].IsFinished)
                    {
                        var number = data[i, k];
                        var ColumnVals = new List<int>();

                        for (int l = 0; l < 9; l++)
                        {
                            if (data[l, k].IsFinished)
                                ColumnVals.Add(data[l, k].GetPossibleValues[0]);
                        }


                        foreach (var num in data[i, k].GetPossibleValues.Where(x => ColumnVals.Contains(x)).ToList())
                        {
                            data[i, k].EliminateNumber(num);
                            checkChanged = true;
                        }
                    }
                }

                // count finished values
                var finished = new List<int>();
                for (int k = 0; k < 9; k++)
                {
                    if (data[i, k].IsFinished)
                        finished.Add(data[i, k].GetPossibleValues[0]);
                }
                if (finished.Count == 8)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        if (!data[i, k].IsFinished)
                        {
                            data[i, k].SetNumber(Arr1to9.Except(finished).First());
                            data[i, k].SetDirty();
                            checkChanged = true;
                            break;
                        }
                    }
                }
            }

            // recheck columns
            for (int k = 0; k < 9; k++)
            {
                // eliminate values
                for (int i = 0; i < 9; i++) // lines
                {
                    if (!data[i, k].IsFinished) // if unknown
                    {
                        var number = data[i, k];
                        var ColumnVals = new List<int>();

                        for (int l = 0; l < 9; l++)
                        {
                            if (data[l, k].IsFinished)
                                ColumnVals.Add(data[l, k].GetPossibleValues[0]);
                        }


                        foreach (var num in data[i, k].GetPossibleValues.Where(x => ColumnVals.Contains(x)).ToList())
                        {
                            data[i, k].EliminateNumber(num);
                            checkChanged = true;
                        }
                    }
                }

                // count finished values
                var finished = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    if (data[i, k].IsFinished)
                        finished.Add(data[i, k].GetPossibleValues[0]);
                }
                if (finished.Count == 8)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (!data[i, k].IsFinished)
                        {
                            data[i, k].SetNumber(Arr1to9.Except(finished).First());
                            data[i, k].SetDirty();
                            checkChanged = true;
                            break;
                        }
                    }
                }
            }

            // reqcheck 3x3 squares
            for (int i = 0; i < 9; i++)
            {
                int offsetX = (i % 3) * 3;
                int offsetY = (i / 3) * 3;
                var finished = new List<int>();

                for (int l = 0; l < 3; l++) // lines
                {
                    for (int c = 0; c < 3; c++) // columns
                    {
                        var number = data[offsetY + l, offsetX + c];
                        if (number.IsFinished)
                            finished.Add(number.GetPossibleValues[0]);
                    }
                }

                if (finished.Count == 8)
                {
                    for (int l = 0; l < 3; l++) // lines
                    {
                        for (int c = 0; c < 3; c++) // columns
                        {
                            var number = data[offsetY + l, offsetX + c];
                            if (data[offsetY + l, offsetX + c].IsFinished)
                            {
                                data[offsetY + l, offsetX + c].SetNumber(Arr1to9.Except(finished).First());
                                data[offsetY + l, offsetX + c].SetDirty();
                                checkChanged = true;
                                break;
                            }
                        }
                    }
                }
            }

            return checkChanged;
        }
    }
}
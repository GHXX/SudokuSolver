using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    class SudokuNumberStack
    {
        List<int> possibleValues;
        bool isDirty = false;
        bool isFinished = false;

        public SudokuNumberStack()
        {
            possibleValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public override string ToString()
        {
            return $"{(string.Join(",", possibleValues))}";
        }

        public void EliminateNumber(int number)
        {
            if (!isFinished)
            {
                if (possibleValues.Contains(number))
                {
                    possibleValues.Remove(number);
                    isDirty = true;
                    if (possibleValues.Count == 1)
                    {
                        isFinished = true;
                    }
                }
                else
                {
                    throw new System.InvalidOperationException("Cannot eliminate number that doesnt exist!");
                }
            }
            else
            {
                throw new System.InvalidOperationException("Cannot eliminate number because there is only one left!");
            }
        }

        internal void SetDirty() => isDirty = true;

        public void SetNumber(int number, bool force = false)
        {
            if (IsFinished && !force)
            {
                throw new InvalidOperationException("Cannot set number if finished already!");
            }
            possibleValues.Clear();
            possibleValues.Add(number);
            isFinished = true;
            isDirty = true;
        }

        public List<int> GetPossibleValues => possibleValues;
        public bool IsDirty => isDirty;
        public bool IsFinished => isFinished;

        public void MarkClean() => isDirty = false;

        public string GetShort()
        {
            return this.GetPossibleValues.Count > 1 ? "#" : this.GetPossibleValues[0].ToString();
        }
    }
}
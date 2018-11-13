using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    class SudokuNumberStack
    {
        List<int> possibleValues;
        bool isDirty = false;
        bool isFinished = false;

        public SudokuNumberStack()
        {
            this.possibleValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public override string ToString()
        {
            return $"{(string.Join(",", this.possibleValues))}";
        }

        public void EliminateNumber(int number)
        {
            if (!this.isFinished)
            {
                if (this.possibleValues.Contains(number))
                {
                    this.possibleValues.Remove(number);
                    this.isDirty = true;
                    if (this.possibleValues.Count == 1)
                    {
                        this.isFinished = true;
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

        internal void SetDirty() => this.isDirty = true;

        public void SetNumber(int number, bool force = false)
        {
            if (this.IsFinished && !force)
            {
                throw new InvalidOperationException("Cannot set number if finished already!");
            }
            this.possibleValues.Clear();
            this.possibleValues.Add(number);
            this.isFinished = true;
            this.isDirty = true;
        }

        public List<int> PossibleValues => this.possibleValues;
        public bool IsDirty => this.isDirty;
        public bool IsFinished => this.isFinished;

        public void MarkClean() => this.isDirty = false;

        public string GetShort(bool listAllValues)
        {
            return this.PossibleValues.Count > 1 ? (listAllValues ? $"{{{string.Join(",", this.PossibleValues)}}}" : "#") : this.PossibleValues[0].ToString();
        }
    }
}
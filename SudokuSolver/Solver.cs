using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    class Solver
    {
        SudokuContents content;
        Panel p;

        public Solver(Panel panel)
        {
            p = panel;
            UpdateDrawing();
            var knownValues = new int[9,9];
            knownValues[1, 1] = 2;
            knownValues[2, 6] = 5;
            knownValues[3, 0] = 1;
            knownValues[4, 2] = 5;
            knownValues[5, 5] = 3;
            knownValues[6, 1] = 8;
            knownValues[7, 8] = 7;
            knownValues[8, 7] = 2;
            knownValues[0, 0] = 5;
            knownValues[1, 2] = 9;
            knownValues[2, 3] = 2;
            knownValues[3, 4] = 1;
            knownValues[4, 8] = 5;
            content = new SudokuContents(knownValues);
        }

        internal void Start()
        {
            SolveCycleMain(); // run on thread
        }

        void SolveCycleMain()
        {
            while (content.RecheckValues()) // run until done
            {
                UpdateDrawing();
            }
            UpdateDrawing();
        }

        private void UpdateDrawing()
        {
            p.Update();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var l = new Label
                    {
                        Size = new Size(5, 5),
                        Text = "lbl_dyn_" + (i * 9 + j).ToString()
                    };
                    p.Controls.Add(l);
                }
            }
        }
    }
}

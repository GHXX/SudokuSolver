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
            var knownValues = new int[9, 9];
            // y,x
            knownValues[0, 0] =7 ;
            knownValues[0, 3] = 6;
            knownValues[0, 8] = 2;
            knownValues[1, 1] = 3;
            knownValues[1, 3] = 2;
            knownValues[1, 7] = 7;
            knownValues[2, 2] = 8;
            knownValues[2, 3] = 1;
            knownValues[2, 4] = 7;
            knownValues[2, 6] = 4;
            knownValues[3, 6] = 8;
            knownValues[3, 7] = 4;
            knownValues[3, 8] = 6;
            knownValues[4, 2] = 3;
            knownValues[4, 4] = 1;
            knownValues[4, 6] = 9;
            knownValues[5, 0] = 5;
            knownValues[5, 1] = 4;
            knownValues[5, 2] = 2;
            knownValues[6, 2] = 5;
            knownValues[6, 4] = 3;
            knownValues[6, 5] = 9;
            knownValues[6, 6] = 2;
            knownValues[7, 1] = 7;
            knownValues[7, 5] = 4;
            knownValues[7, 7] = 9;
            knownValues[8, 0] = 2;
            knownValues[8, 5] = 1;
            knownValues[8, 8] = 7;
            content = new SudokuContents(knownValues);
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
                UpdateDrawing();
            }
            UpdateDrawing();
        }

        private void UpdateDrawing()
        {
            p.Invoke((MethodInvoker)delegate
           {
               p.Controls.Clear();
               for (int i = 0; i < 9; i++)
               {
                   for (int j = 0; j < 9; j++)
                   {
                       var l = new Label
                       {
                           Size = new Size(12, 12),
                           Text = (this.content?.Data[j,i].GetShort() ?? "?").ToString(),
                           Location = new Point(j*24,i*24)
                       };
                       l.Show();
                       p.Controls.Add(l);
                   }
               }
               p.Update();
           });
        }
    }
}

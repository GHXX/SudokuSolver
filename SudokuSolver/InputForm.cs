using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class InputForm : Form
    {
        private readonly int[] numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Dictionary<GroupBox, int> values = new Dictionary<GroupBox, int>();
        public int[,] result = new int[9, 9];

        GroupBox mainGroupBox = null;

        public InputForm()
        {
            InitializeComponent();
            CreateElements();
        }

        private void CreateElements()
        {
            const int smallBoxSize = 40;
            const int outerSpacer = 8;
            const int bigSpacer = 8;
            const int smallSpacer = 7;
            int bigBoxSize = bigSpacer * 2 + 3 * (smallSpacer * 2 + smallBoxSize * 3 + 2 * smallSpacer) + 2 * outerSpacer;
            this.mainGroupBox = new GroupBox
            {
                Parent = this,
                Location = new Point(10, 10),
                Size = new Size(bigBoxSize, bigBoxSize)
            };
            int medBoxSize = smallSpacer * 2 + smallBoxSize * 3 + 2 * smallSpacer;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    var mediumGb = new GroupBox
                    {
                        Parent = this.mainGroupBox,
                        Location = new Point(outerSpacer + x * (medBoxSize + bigSpacer), outerSpacer + y * (medBoxSize + bigSpacer)),
                        Size = new Size(medBoxSize, medBoxSize)
                    };

                    for (int x2 = 0; x2 < 3; x2++)
                    {
                        for (int y2 = 0; y2 < 3; y2++)
                        {
                            var smallGb = new GroupBox
                            {
                                Parent = mediumGb,
                                Location = new Point(smallSpacer + x2 * smallBoxSize + x2 * smallSpacer, smallSpacer + y2 * smallBoxSize + y2 * smallSpacer),
                                Size = new Size(smallBoxSize, smallBoxSize)
                            };
                            this.values.Add(smallGb, 0);

                            var lbl = new Label
                            {
                                Parent = smallGb,
                                Width = 10
                            };
                            lbl.Location = new Point(smallGb.Height / 2 - lbl.Height / 2, smallGb.Width / 2 - lbl.Width / 2);
                            smallGb.Click += SmallGb_Click;
                            lbl.Click += Lbl_Click;
                        }
                    }
                }
            }
        }

        private void Lbl_Click(object sender, EventArgs e)
        {
            var lbl = (Label)sender;
            SmallGb_Click(lbl.Parent, e);
        }

        private void SmallGb_Click(object sender, EventArgs e)
        {
            var ea = e as MouseEventArgs;
            var gb = (GroupBox)sender;
            this.values[gb] = ((this.values[gb] + (ea.Button == MouseButtons.Left ? 1 : (ea.Button == MouseButtons.Right ? -1 : 0))) + 10) % 10;
            gb.Controls[0].Text = this.values[gb] == 0 ? "" : this.values[gb].ToString();
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            int smallIndex, bigIndex = 0;

            foreach (GroupBox subBox in this.mainGroupBox.Controls)
            {
                smallIndex = 0;
                foreach (GroupBox smallBox in subBox.Controls)
                {
                    this.result[smallIndex / 3 + 3 * (bigIndex / 3), smallIndex % 3 + (bigIndex % 3) * 3] = this.values[smallBox];
                    smallIndex++;
                }
                bigIndex++;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

using System;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class MainForm : Form
    {
        Solver solver;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.solver = new Solver(this.panel_SudokuMainArea);
        }

        private void Btn_Calc_Click(object sender, EventArgs e)
        {
            this.solver.Start();
        }

        private void Btn_Load_Click(object sender, EventArgs e)
        {
            var inForm = new InputForm();
            var result = inForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.solver.Load(inForm.result);
            }
        }
    }
}

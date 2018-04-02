namespace SudokuSolver
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_SudokuMainArea = new System.Windows.Forms.Panel();
            this.btn_Calc = new System.Windows.Forms.Button();
            this.lblValid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel_SudokuMainArea
            // 
            this.panel_SudokuMainArea.Location = new System.Drawing.Point(12, 12);
            this.panel_SudokuMainArea.Name = "panel_SudokuMainArea";
            this.panel_SudokuMainArea.Size = new System.Drawing.Size(460, 460);
            this.panel_SudokuMainArea.TabIndex = 0;
            // 
            // btn_Calc
            // 
            this.btn_Calc.Location = new System.Drawing.Point(12, 484);
            this.btn_Calc.Name = "btn_Calc";
            this.btn_Calc.Size = new System.Drawing.Size(75, 23);
            this.btn_Calc.TabIndex = 1;
            this.btn_Calc.Text = "Calculate";
            this.btn_Calc.UseVisualStyleBackColor = true;
            this.btn_Calc.Click += new System.EventHandler(this.Btn_Calc_Click);
            // 
            // lblValid
            // 
            this.lblValid.AutoSize = true;
            this.lblValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.lblValid.Location = new System.Drawing.Point(610, 12);
            this.lblValid.Name = "lblValid";
            this.lblValid.Size = new System.Drawing.Size(104, 31);
            this.lblValid.TabIndex = 2;
            this.lblValid.Text = "Valid: ?";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblValid);
            this.Controls.Add(this.btn_Calc);
            this.Controls.Add(this.panel_SudokuMainArea);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Sudoku Solver";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_SudokuMainArea;
        private System.Windows.Forms.Button btn_Calc;
        private System.Windows.Forms.Label lblValid;
    }
}


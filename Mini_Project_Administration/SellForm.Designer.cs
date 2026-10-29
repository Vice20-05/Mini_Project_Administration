namespace Mini_Project_Administration
{
    partial class SellForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDetails = new System.Windows.Forms.Label();
            this.lblSell = new System.Windows.Forms.Label();
            this.numSell = new System.Windows.Forms.NumericUpDown();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSell)).BeginInit();
            this.SuspendLayout();
            //
            // lblDetails
            //
            this.lblDetails.Location = new System.Drawing.Point(12, 12);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(320, 125);
            this.lblDetails.Text = "details";
            //
            // lblSell
            //
            this.lblSell.AutoSize = true;
            this.lblSell.Location = new System.Drawing.Point(12, 147);
            this.lblSell.Name = "lblSell";
            this.lblSell.Size = new System.Drawing.Size(84, 13);
            this.lblSell.Text = "Quantity to sell:";
            //
            // numSell
            //
            this.numSell.Location = new System.Drawing.Point(105, 145);
            this.numSell.Name = "numSell";
            this.numSell.Size = new System.Drawing.Size(100, 20);
            this.numSell.TabIndex = 0;
            //
            // btnSell
            //
            this.btnSell.Location = new System.Drawing.Point(176, 180);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(75, 23);
            this.btnSell.TabIndex = 1;
            this.btnSell.Text = "Sell";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(257, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // SellForm
            //
            this.AcceptButton = this.btnSell;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(345, 215);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.lblSell);
            this.Controls.Add(this.numSell);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SellForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sell product";
            ((System.ComponentModel.ISupportInitialize)(this.numSell)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Label lblSell;
        private System.Windows.Forms.NumericUpDown numSell;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnCancel;
    }
}

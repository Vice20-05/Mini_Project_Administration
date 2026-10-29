namespace Mini_Project_Administration
{
    partial class AddForm
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
            this.lblQuantity = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblId = new System.Windows.Forms.Label();
            this.numId = new System.Windows.Forms.NumericUpDown();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblEntryDate = new System.Windows.Forms.Label();
            this.dtpEntryDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpiration = new System.Windows.Forms.Label();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numId)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(12, 17);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity:";
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(100, 15);
            this.numQuantity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(100, 20);
            this.numQuantity.TabIndex = 0;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(12, 47);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(59, 13);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "Product Id:";
            // 
            // numId
            // 
            this.numId.Location = new System.Drawing.Point(100, 45);
            this.numId.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numId.Name = "numId";
            this.numId.Size = new System.Drawing.Size(100, 20);
            this.numId.TabIndex = 1;
            this.numId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 47);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(100, 45);
            this.txtName.MaxLength = 30;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 20);
            this.txtName.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 77);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(100, 75);
            this.txtDescription.MaxLength = 500;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(260, 60);
            this.txtDescription.TabIndex = 3;
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Location = new System.Drawing.Point(12, 148);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(58, 13);
            this.lblEntryDate.TabIndex = 4;
            this.lblEntryDate.Text = "Entry date:";
            // 
            // dtpEntryDate
            // 
            this.dtpEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEntryDate.Location = new System.Drawing.Point(100, 145);
            this.dtpEntryDate.Name = "dtpEntryDate";
            this.dtpEntryDate.Size = new System.Drawing.Size(120, 20);
            this.dtpEntryDate.TabIndex = 4;
            // 
            // lblExpiration
            // 
            this.lblExpiration.AutoSize = true;
            this.lblExpiration.Location = new System.Drawing.Point(12, 178);
            this.lblExpiration.Name = "lblExpiration";
            this.lblExpiration.Size = new System.Drawing.Size(80, 13);
            this.lblExpiration.TabIndex = 5;
            this.lblExpiration.Text = "Expiration date:";
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiration.Location = new System.Drawing.Point(100, 175);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(120, 20);
            this.dtpExpiration.TabIndex = 5;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 208);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "Category:";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(100, 205);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(260, 21);
            this.cboCategory.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(204, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(285, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AddForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(375, 275);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.numId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblEntryDate);
            this.Controls.Add(this.dtpEntryDate);
            this.Controls.Add(this.lblExpiration);
            this.Controls.Add(this.dtpExpiration);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddForm";
            this.Load += new System.EventHandler(this.AddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.NumericUpDown numId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblEntryDate;
        private System.Windows.Forms.DateTimePicker dtpEntryDate;
        private System.Windows.Forms.Label lblExpiration;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}

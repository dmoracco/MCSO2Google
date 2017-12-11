namespace MCSO.Scheduling.Forms
{
    partial class EditSubForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSubForm));
            this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
            this.comboBoxDesignation = new System.Windows.Forms.ComboBox();
            this.textBoxStartDate = new System.Windows.Forms.TextBox();
            this.textBoxEndDate = new System.Windows.Forms.TextBox();
            this.buttonCalendarStart = new System.Windows.Forms.Button();
            this.buttonCalendarEnd = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxEmployee
            // 
            this.comboBoxEmployee.FormattingEnabled = true;
            this.comboBoxEmployee.Location = new System.Drawing.Point(9, 39);
            this.comboBoxEmployee.Name = "comboBoxEmployee";
            this.comboBoxEmployee.Size = new System.Drawing.Size(187, 21);
            this.comboBoxEmployee.TabIndex = 0;
            this.comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(this.comboBoxEmployee_SelectedIndexChanged);
            // 
            // comboBoxDesignation
            // 
            this.comboBoxDesignation.FormattingEnabled = true;
            this.comboBoxDesignation.Items.AddRange(new object[] {
            "A: 7A-3P",
            "B: 3P-11P",
            "C: 11P-7A",
            "D: 7A-7P",
            "E: 7P-7A",
            "F: 11P-7A",
            "G: 11P-11A",
            "I: 7P-3A",
            "X: Other",
            "Vacation",
            "SL",
            "FSL",
            "Training",
            "Admin",
            "Funeral Leave"});
            this.comboBoxDesignation.Location = new System.Drawing.Point(11, 76);
            this.comboBoxDesignation.Name = "comboBoxDesignation";
            this.comboBoxDesignation.Size = new System.Drawing.Size(186, 21);
            this.comboBoxDesignation.TabIndex = 1;
            this.comboBoxDesignation.SelectedIndexChanged += new System.EventHandler(this.comboBoxDesignation_SelectedIndexChanged);
            // 
            // textBoxStartDate
            // 
            this.textBoxStartDate.Location = new System.Drawing.Point(11, 120);
            this.textBoxStartDate.Name = "textBoxStartDate";
            this.textBoxStartDate.Size = new System.Drawing.Size(185, 20);
            this.textBoxStartDate.TabIndex = 2;
            // 
            // textBoxEndDate
            // 
            this.textBoxEndDate.Location = new System.Drawing.Point(12, 159);
            this.textBoxEndDate.Name = "textBoxEndDate";
            this.textBoxEndDate.Size = new System.Drawing.Size(184, 20);
            this.textBoxEndDate.TabIndex = 3;
            // 
            // buttonCalendarStart
            // 
            this.buttonCalendarStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonCalendarStart.Image")));
            this.buttonCalendarStart.Location = new System.Drawing.Point(202, 112);
            this.buttonCalendarStart.Name = "buttonCalendarStart";
            this.buttonCalendarStart.Size = new System.Drawing.Size(35, 34);
            this.buttonCalendarStart.TabIndex = 4;
            this.buttonCalendarStart.UseVisualStyleBackColor = true;
            this.buttonCalendarStart.Click += new System.EventHandler(this.buttonCalendarStart_Click);
            // 
            // buttonCalendarEnd
            // 
            this.buttonCalendarEnd.Image = ((System.Drawing.Image)(resources.GetObject("buttonCalendarEnd.Image")));
            this.buttonCalendarEnd.Location = new System.Drawing.Point(202, 151);
            this.buttonCalendarEnd.Name = "buttonCalendarEnd";
            this.buttonCalendarEnd.Size = new System.Drawing.Size(35, 34);
            this.buttonCalendarEnd.TabIndex = 5;
            this.buttonCalendarEnd.UseVisualStyleBackColor = true;
            this.buttonCalendarEnd.Click += new System.EventHandler(this.buttonCalendarEnd_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(202, 218);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(63, 32);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(133, 217);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // EditSubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.buttonCalendarEnd);
            this.Controls.Add(this.buttonCalendarStart);
            this.Controls.Add(this.textBoxEndDate);
            this.Controls.Add(this.textBoxStartDate);
            this.Controls.Add(this.comboBoxDesignation);
            this.Controls.Add(this.comboBoxEmployee);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EditSubForm";
            this.Text = "Add Shift";
            this.Load += new System.EventHandler(this.EditSubForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxEmployee;
        private System.Windows.Forms.ComboBox comboBoxDesignation;
        private System.Windows.Forms.TextBox textBoxStartDate;
        private System.Windows.Forms.TextBox textBoxEndDate;
        private System.Windows.Forms.Button buttonCalendarStart;
        private System.Windows.Forms.Button buttonCalendarEnd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
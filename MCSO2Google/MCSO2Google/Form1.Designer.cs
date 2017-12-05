namespace MCSO2Google
{
    partial class Form1
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
            this.btnOpenCSV = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.textBoxCSVPath = new System.Windows.Forms.TextBox();
            this.textBoxGoogleAcct = new System.Windows.Forms.TextBox();
            this.listViewShifts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnOpenCSV
            // 
            this.btnOpenCSV.Location = new System.Drawing.Point(385, 16);
            this.btnOpenCSV.Name = "btnOpenCSV";
            this.btnOpenCSV.Size = new System.Drawing.Size(75, 23);
            this.btnOpenCSV.TabIndex = 0;
            this.btnOpenCSV.Text = "Open";
            this.btnOpenCSV.UseVisualStyleBackColor = true;
            this.btnOpenCSV.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(385, 45);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textBoxCSVPath
            // 
            this.textBoxCSVPath.Location = new System.Drawing.Point(95, 18);
            this.textBoxCSVPath.Name = "textBoxCSVPath";
            this.textBoxCSVPath.Size = new System.Drawing.Size(275, 20);
            this.textBoxCSVPath.TabIndex = 3;
            // 
            // textBoxGoogleAcct
            // 
            this.textBoxGoogleAcct.Enabled = false;
            this.textBoxGoogleAcct.Location = new System.Drawing.Point(95, 47);
            this.textBoxGoogleAcct.Name = "textBoxGoogleAcct";
            this.textBoxGoogleAcct.Size = new System.Drawing.Size(275, 20);
            this.textBoxGoogleAcct.TabIndex = 4;
            // 
            // listViewShifts
            // 
            this.listViewShifts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewShifts.FullRowSelect = true;
            this.listViewShifts.GridLines = true;
            this.listViewShifts.Location = new System.Drawing.Point(12, 88);
            this.listViewShifts.Name = "listViewShifts";
            this.listViewShifts.Size = new System.Drawing.Size(579, 266);
            this.listViewShifts.TabIndex = 5;
            this.listViewShifts.UseCompatibleStateImageBehavior = false;
            this.listViewShifts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Shift";
            this.columnHeader3.Width = 35;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Start Time";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "End Time";
            this.columnHeader5.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 433);
            this.Controls.Add(this.listViewShifts);
            this.Controls.Add(this.textBoxGoogleAcct);
            this.Controls.Add(this.textBoxCSVPath);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnOpenCSV);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenCSV;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox textBoxCSVPath;
        private System.Windows.Forms.TextBox textBoxGoogleAcct;
        private System.Windows.Forms.ListView listViewShifts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}


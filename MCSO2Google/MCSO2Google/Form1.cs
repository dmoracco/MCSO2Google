using MCSO.Scheduling.CSV.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scheduler;

namespace MCSO2Google
{
    public partial class Form1 : Form
    {
        private CSVFile _schedule;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            
            //Open File Dialog and create Schedule
            OpenFileDialog openfiledialog = new OpenFileDialog
            {
                DefaultExt = "csv",
                Filter = "Comma Separated Value files | *.csv"               
            };

            string path = "";

            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    path = openfiledialog.FileName;
                    textBoxCSVPath.Text = path;
                    _schedule = new CSVFile(path);
                    _schedule.PopulateSchedule(t)
                    btnConnect.Enabled = true;
                    textBoxGoogleAcct.Enabled = true;

                    listViewShifts_refresh();
                    
                }
                catch (Exception ex)
                {
                    string msg = ex + "Exception thrown.";
                    MessageBox.Show(msg, "Open CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                path = "";
                textBoxCSVPath.Text = "";
            }

         
        }
        private void listViewShifts_refresh()
        {
            listViewShifts.Items.Clear();

            foreach (WorkWeek week in _schedule._workWeeks)
            {
                foreach (WorkDay day in week._workDays)
                {
                    foreach (Shift shift in day._shifts)
                    {
                        var displayshift = new ListViewItem(shift.ShiftDate.ToString());

                        displayshift.SubItems.Add(shift._employee.Name);
                        displayshift.SubItems.Add(shift.ShiftDesignation);
                        displayshift.SubItems.Add(shift.StartDateTime.ToString());
                        displayshift.SubItems.Add(shift.EndDateTime.ToString());

                        listViewShifts.Items.Add(displayshift);
                    }
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "Change")
            {
                DialogResult disconnectresult = MessageBox.Show("Log out of current Google Account?", "Google Account", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (disconnectresult == DialogResult.Yes)
                {
                    _schedule._cloudCalendar.ClearAccount();
                    btnConnect.Text = "Connect";
                }                
            }
            _schedule.ConnectGoogle();
            string acctID = _schedule._cloudCalendar.GetAccountEmail();
            textBoxGoogleAcct.Text = acctID;
            btnConnect.Text = "Change";
            btnUpload.Enabled = true;
            btnAdd.Enabled = true;
           
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            toolStripStatusUpload.Text = "Uploading...";
            _schedule.UploadCalendar();
            toolStripStatusUpload.Text = "Finished!";
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var Form2 = new Form2())
            {
                Form2.EmployeeList = _schedule._employees;
                var result = Form2.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _schedule.AddShift(Form2.Shift);
                    listViewShifts_refresh();
                    
                }
            }
        }
    }
}

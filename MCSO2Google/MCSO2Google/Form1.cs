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
        private Schedule _schedule;
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
                    _schedule = new Schedule(path);
                    btnConnect.Enabled = true;
                    textBoxGoogleAcct.Enabled = true;

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

            



            

            
            //test.ConnectGoogle();
            //test.UploadCalendar();

            //List<string> events = test._cloudCalendar.ListEvents("primary");

         
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _schedule.ConnectGoogle();
            string acctID = _schedule._cloudCalendar.GetAccountEmail();
            textBoxGoogleAcct.Text = acctID;
        }
    }
}

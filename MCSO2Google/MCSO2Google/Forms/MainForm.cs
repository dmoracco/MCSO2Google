﻿using MCSO.Scheduling.FileIO;
using MCSO.Scheduling.ScheduleBase;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Windows.Forms;

namespace MCSO.Scheduling.Forms
{
    public partial class MainForm : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Schedule _currentSchedule;

        public MainForm()
        {
            InitializeComponent();            
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
                    _currentSchedule.GoogleCalendar.ClearCredentials();
                    btnConnect.Text = "Connect";
                }
            }

            _currentSchedule = new Schedule();
            textBoxGoogleAcct.Text = _currentSchedule.GoogleCalendar.GetAccountEmail();
        
            btnConnect.Text = "Change";
            btnOpenCSV.Enabled = true;
            textBoxCSVPath.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            
            //Open File Dialog
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

                    var file = new CSVFile(path);
                    file.PopulateSchedule(_currentSchedule);

                    listViewShifts_refresh();
                    
                }
                catch (Exception ex)
                {
                    log.Debug("Error while opening/parsing CSV", ex);
                    MessageBox.Show("Error opening/parsing CSV", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                path = "";
                textBoxCSVPath.Text = "";
            }

            btnUpload.Enabled = true;

        }
        private void listViewShifts_refresh()
        {
            listViewShifts.Items.Clear();

            foreach (WorkWeek week in _currentSchedule.WorkWeekList)
            {
                foreach (WorkDay day in week.WorkDayList)
                {
                    foreach (Shift shift in day.ShiftList)
                    {
                        var displayshift = new ListViewItem(shift.StartDateTime.Date.ToString("d"));

                        displayshift.SubItems.Add(shift.Employee.Name);
                        displayshift.SubItems.Add(shift.ShiftDesignation);
                        displayshift.SubItems.Add(shift.StartDateTime.TimeOfDay.ToString("hh\\:mm"));
                        displayshift.SubItems.Add(shift.EndDateTime.TimeOfDay.ToString("hh\\:mm"));
                        displayshift.SubItems.Add(shift.ControlNumber.ToString());
                        listViewShifts.Items.Add(displayshift);
                    }
                }
            }
            if (listViewShifts.Items.Count > 0)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                btnUpload.Enabled = true;
                btnAdd.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnUpload.Enabled = false;
                btnAdd.Enabled = false;
            }
        }
       

        private void btnUpload_Click(object sender, EventArgs e)
        {
            toolStripStatusUpload.Text = "Uploading...";
            _currentSchedule.UploadSchedule();
            toolStripStatusUpload.Text = "Finished!";
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var addform = new EditSubForm())
            {
                addform.Operation = "Add";
                addform.EmployeeList = _currentSchedule.EmployeeList;
                var result = addform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _currentSchedule.AddShift(addform.Employee, addform.Start, addform.End, addform.ShiftDesignation);
     
                    listViewShifts_refresh();

                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            ListViewItem c = listViewShifts.SelectedItems[0];
            string message = String.Format("Remove shift: {0} for {1} {2} to {3}", 
                c.Text, c.SubItems[1].Text, c.SubItems[3].Text, c.SubItems[4].Text);

            DialogResult confirmationresult = MessageBox.Show(message, "Confirm?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmationresult == DialogResult.Yes)
            {
                int controlID = Convert.ToInt32(c.SubItems[5].Text);

                _currentSchedule.RemoveShift(controlID);
                listViewShifts_refresh();
                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var editform = new EditSubForm())
            {
                ListViewItem c = listViewShifts.SelectedItems[0];
                var shiftlist = _currentSchedule.ShiftList();
                int controlID = Convert.ToInt32(c.SubItems[5].Text);
                var matchingshift = shiftlist.Find(x => x.ControlNumber == controlID);

                editform.Operation = "Edit";
                editform.EmployeeList = _currentSchedule.EmployeeList;

                editform.Start = matchingshift.StartDateTime;
                editform.End = matchingshift.EndDateTime;
                editform.Employee = matchingshift.Employee;
                editform.ShiftDesignation = matchingshift.ShiftDesignation;

                var result = editform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _currentSchedule.RemoveShift(matchingshift.ControlNumber);
                    _currentSchedule.AddShift(editform.Employee, editform.Start, editform.End, editform.ShiftDesignation);

                    listViewShifts_refresh();

                }
            }
        }

        private void listViewShifts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
        }

        private void listViewShifts_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _currentSchedule.StoreSchedule();
        }
    }
}

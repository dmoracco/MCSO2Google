using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCSO.Scheduling.Forms
{
    public partial class EditSubForm : Form
    {
        public string Operation { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ShiftDesignation { get; set; }
        public Employee Employee { get; set; }

        public EditSubForm()
        {
            InitializeComponent();
            EmployeeList = new List<Employee>();
            this.CancelButton = btnCancel;
        }

        private void EditSubForm_Load(object sender, EventArgs e)
        {
            //Populate form and set defaults

            foreach (Employee employee in EmployeeList)
            {
                comboBoxEmployee.Items.Add(employee.Name);
            }
            if (Operation == "Add")
            {
                comboBoxDesignation.SelectedIndex = 0;
                comboBoxEmployee.SelectedIndex = 0;
                Employee = EmployeeList[0];
                ShiftDesignation = MCSOstatics.DispatchLegend[0];
            }
            else if (Operation == "Edit")
            {
                comboBoxDesignation.Text = ShiftDesignation;
                comboBoxEmployee.Text = Employee.Name;
                textBoxStartDate.Text = Start.ToString();
                textBoxEndDate.Text = End.ToString();
                Text = "Edit Shift";
            }
        }

        private void buttonCalendarStart_Click(object sender, EventArgs e)
        {
            using (var calendarform = new CalendarSubForm())
            {
                var result = calendarform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DateTime chosentime = calendarform.Time;
                    Start = chosentime;
                    textBoxStartDate.Text = chosentime.ToString();
                }
            }
        }

        private void buttonCalendarEnd_Click(object sender, EventArgs e)
        {
            using (var calendarform = new CalendarSubForm())
            {
                var result = calendarform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    
                    DateTime chosentime = calendarform.Time;
                    End = chosentime;
                    textBoxEndDate.Text = chosentime.ToString();

                }
            }
        }

        private void comboBoxDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShiftDesignation = MCSOstatics.DispatchLegend[comboBoxDesignation.SelectedIndex];
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string message = String.Format("{0} shift for {1} working a \"{2}\" shift on {3}?",
                Operation, Employee.Name, ShiftDesignation, Start.Date.ToString("d"));

            DialogResult confirmationresult = MessageBox.Show(message, "Confirm?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmationresult == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }         
        }

        private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee = EmployeeList[comboBoxEmployee.SelectedIndex];
        }
    }
}

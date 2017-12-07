using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class Form2 : Form
    {
        public List<Employee> EmployeeList { get; set; }
        public Shift Shift { get; set; }
        public DateTime Start { get; set;}
        public DateTime End { get; set; }
        public char ShiftDesignation { get; set; }
        private Employee _selectedEmployee;
        private char[] _designationList = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'I', 'X' };



        public Form2()
        {
            InitializeComponent();
            EmployeeList = new List<Employee>();
            
            
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            foreach (Employee employee in EmployeeList)
            {
                comboBoxEmployee.Items.Add(employee.Name);
            }
            comboBoxDesignation.SelectedIndex = 0;
            comboBoxEmployee.SelectedIndex = 0;
            _selectedEmployee = EmployeeList[0];
            ShiftDesignation = _designationList[0];
        }

        private void buttonCalendarStart_Click(object sender, EventArgs e)
        {
            using (var Form3 = new Form3())
            {
                var result = Form3.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DateTime chosentime = Form3.Time;
                    Start = chosentime;
                    textBoxStartDate.Text = chosentime.ToString();
                }
            }
        }

        private void buttonCalendarEnd_Click(object sender, EventArgs e)
        {
            using (var Form3 = new Form3())
            {
                var result = Form3.ShowDialog();
                if (result == DialogResult.OK)
                {
                    
                    DateTime chosentime = Form3.Time;
                    End = chosentime;
                    textBoxEndDate.Text = chosentime.ToString();

                }
            }
        }

        private void comboBoxDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShiftDesignation = _designationList[comboBoxDesignation.SelectedIndex];
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Shift = new Shift(Start, End, ShiftDesignation, _selectedEmployee);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedEmployee = EmployeeList[comboBoxEmployee.SelectedIndex];
        }
    }
}

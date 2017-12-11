using System;
using System.Windows.Forms;

namespace MCSO.Scheduling.Forms
{

    public partial class CalendarSubForm : Form
    {
        public DateTime Time { get; set; }

        public EditSubForm EditSubForm
        {
            get => default(EditSubForm);
            set
            {
            }
        }

        private DateTime _date;

        public CalendarSubForm()
        {
            InitializeComponent();
      
            this.CancelButton = btnCancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var _time = new TimeSpan();
           
            _time = TimeSpan.Parse(comboBoxHour.Text + ":00");

            if (comboBoxAMPM.SelectedIndex == 1)
            {
                TimeSpan t = new TimeSpan(12, 00, 00);
                _time = _time + t;
            }
                
            Time = _date + _time;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            _date = monthCalendar1.SelectionStart;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBoxHour.SelectedIndex = 6;
            comboBoxAMPM.SelectedIndex = 0;
            _date = monthCalendar1.SelectionStart;
        }
    }
}

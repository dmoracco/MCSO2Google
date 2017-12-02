using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public class WorkWeek : Calendar
	{
		
        private List<WorkDay> _workDays;
        public DateTime WeekStart { get; }

        public WorkWeek(Shift initialshift):this(new WorkDay(initialshift))
        {
      
        }
        public WorkWeek(WorkDay initialday)
        {
            _workDays = new List<WorkDay>();
            if ((int)initialday.Date.DayOfWeek == 0)
                WeekStart = initialday.Date;
            else
            {
                int days = (int)initialday.Date.DayOfWeek;
                DateTime startofweek = initialday.Date.AddDays(-days);
                WeekStart = initialday.Date;
            }

            _workDays.Add(initialday);
        }

        public void AddShift(Shift inputshift)
		{
            if (_workDays.Exists(x => x.Date == inputshift.ShiftDate))
            {
                WorkDay temp = _workDays.Find(y => y.Date == inputshift.ShiftDate);
                temp.AddShift(inputshift);
            }
            else
            {
                _workDays.Add(new WorkDay(inputshift));
            }
		}

		public bool ValidateFullTime()
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public class WorkDay
	{
		private DateTime _date;
        private List<Shift> _shifts;
        private bool _coverage;
        public DateTime Date { get { return _date; } }
        public WorkDay(Shift initialshift)
        {
            _shifts = new List<Shift>();
            _shifts.Add(initialshift);
            _date = initialshift.ShiftDate;
            _coverage = ValidateCoverage();
        }
        public void AddShift(Shift inputshift)
        {
            _shifts.Add(inputshift);
            _coverage = ValidateCoverage();
        }
        public bool ValidateCoverage()
		{
            TimeSpan earliest = _shifts[0].StartDateTime.TimeOfDay;
            TimeSpan latest = _shifts[0].EndDateTime.TimeOfDay;
            TimeSpan EOD = new TimeSpan(23, 59, 59);
            TimeSpan BOD = new TimeSpan(00, 00, 00);

            foreach (Shift shift in _shifts)
            {
                if (shift.StartDateTime.TimeOfDay < earliest)
                    earliest = shift.StartDateTime.TimeOfDay;
                if (shift.EndDateTime.TimeOfDay > latest)
                {
                    if (shift.EndDateTime.Date > _date.Date)
                    {
                        latest = EOD;
                    }
                    else
                        latest = shift.EndDateTime.TimeOfDay;
                }                  
                
            }
            if (earliest == BOD && latest == EOD)
                return true;
            else
                return false;
        }
	}
}

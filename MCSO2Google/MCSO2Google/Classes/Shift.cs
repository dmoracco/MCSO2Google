using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class Shift
    {
        private char _designation;
        private DateTime _start;
        private DateTime _end;
        private bool _partialShift;
        public DateTime StartDateTime { get { return _start; } }
        public DateTime EndDateTime { get { return _end; } }
        public DateTime ShiftDate { get {return _start.Date;} }
        public Shift(DateTime start, DateTime end, char shiftID)
        {
            //validate these DateTime inputs
            _start = start;
            _end = end;
            _designation = shiftID;
            _partialShift = false;
        }

        public DateTime PartOfWeek()
        {
            if ((int)this._start.DayOfWeek == 0)
                return _start.Date;
            else
            {
                int days = (int)this._start.DayOfWeek;
                DateTime startofweek = this._start.AddDays(-days);
                return startofweek.Date;
            }
        }
	}
}

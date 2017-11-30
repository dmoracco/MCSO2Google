using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public class Shift
	{
		private char _designation;
		private DateTime _startHour;
		private DateTime _endHour;

        public Shift(DateTime start, DateTime end, char shiftID)
        {
            //validate these DateTime inputs
            _startHour = start;
            _endHour = end;
            _designation = shiftID;
        }
	}
}

using System;
using System.Collections;
using System.Text;

namespace Scheduler
{
	public class WorkWeek : Calendar
	{
		private bool _coverage;
        private ArrayList _workDays = new ArrayList(6);

        public WorkWeek()
        {
            for (int i = 0; i < 7; i++)
            {
                _workDays[i] = new WorkDay();
            }
        }
        public Shift CreateShift()
		{
			throw new NotImplementedException();
		}

		public bool ValidateFullTime()
		{
			throw new NotImplementedException();
		}
	}
}

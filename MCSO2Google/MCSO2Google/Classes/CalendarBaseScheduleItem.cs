using System;
using System.Collections.Generic;
using System.Text;
using Scheduler;

namespace MCSO.Scheduling
{
    public abstract class CalendarBaseScheduleItem
    {
        protected virtual DateTime StartDate { get; set; }
        protected virtual DateTime EndDate { get; set; }
        protected virtual GoogleCalendarAPI GoogleCalendar {get; set;}

        public abstract void AddShift(int employeenumber, DateTime starttime, DateTime endtime, string shiftdesignation);

        /// <summary>
        /// Attaches Google Calendar account to Schedule Item
        /// </summary>
        public void ConnectGoogle()
        {
            GoogleCalendar = new GoogleCalendarAPI();
        }

    }
}

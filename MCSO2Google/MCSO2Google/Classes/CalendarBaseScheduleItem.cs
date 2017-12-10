using MCSO.Scheduling.GoogleAPI;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace MCSO.Scheduling
{
    public abstract class CalendarBaseScheduleItem
    {
        internal protected virtual DateTime StartDate { get; set; }
        protected virtual DateTime EndDate { get; set; }
        protected virtual GoogleCalendarAPI GoogleCalendar { get; set; }

        /// <summary>
        /// Creates new shift element and adds it to date respective container.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="shiftdesignation"></param>
        public void AddShift(Employee employee, DateTime starttime, DateTime endtime, string shiftdesignation)
        {
            // Create new Shift element.
            var newshift = new Shift(employee, starttime, endtime, shiftdesignation);
            AddShift(newshift);
        }
        public abstract void AddShift(Shift newshift);

        /// <summary>
        /// Attaches Google Calendar account to Schedule Item
        /// </summary>
        public void ConnectGoogle()
        {
            GoogleCalendar = new GoogleCalendarAPI();
        }

    }
}

using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSO.Scheduling.ScheduleBase.Data
{
    /// <summary>
    /// Formats Shift to Google Calendar API Event type.
    /// </summary>
    public class CalendarShiftEntry
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Event with Shift information to be entered into Google Calendar
        /// </summary>
        public Event ShiftEvent { get; }
        /// <summary>
        /// Employee assigned to this current Shift Entry.
        /// </summary>
        public string EmployeeName { get; }
        /// <summary>
        /// Employee's Google SubCalendar ID this event should be uploaded to.
        /// </summary>
        public string SubCalendarID { get; set; }

        public CalendarShiftEntry(Shift shift)
        {
            log.Info("Creating new CalendarShiftEntry");
            SubCalendarID = shift.Employee.SubCalendarID;
            EmployeeName = shift.Employee.Name;

            ShiftEvent = new Event()
            {
                Summary = shift.ShiftDesignation + " Shift",
                Location = MCSOstatics.Address,
                Start = new EventDateTime()
                {
                    DateTime = shift.StartDateTime,
                    TimeZone = MCSOstatics.IANATimeZone,

                },
                End = new EventDateTime()
                {
                    DateTime = shift.EndDateTime,
                    TimeZone = MCSOstatics.IANATimeZone,
                }

            };
        }
    }
}

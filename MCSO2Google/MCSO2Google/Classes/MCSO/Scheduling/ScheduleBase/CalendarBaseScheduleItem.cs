using MCSO.Scheduling.GoogleAPI;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace MCSO.Scheduling.ScheduleBase
{
    public abstract class CalendarBaseScheduleItem
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal protected virtual DateTime StartDate { get; set; }
        protected virtual DateTime EndDate { get; set; }
        internal protected virtual GoogleCalendarAPI GoogleCalendar { get; set; }

        /// <summary>
        /// Creates new shift element and adds it to date respective container.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="shiftdesignation"></param>
        public void AddShift(Employee employee, DateTime starttime, DateTime endtime, string shiftdesignation, int controlnumber)
        {
            log.Info("Call for Base::AddShift(Employee, DateTime, DateTime, string, int) method");
            // Create new Shift element.
            try
            {
                var newshift = new Shift()
                {
                    Employee = employee,
                    StartDateTime = starttime,
                    EndDateTime = endtime,
                    ShiftDesignation = shiftdesignation,
                    ControlNumber = controlnumber,
                };

                AddShift(newshift);
            }
            catch (Exception ex)
            {
                log.Debug("Failed to create Shift", ex);
            }
            
            
        }
        public abstract void AddShift(Shift newshift);

        /// <summary>
        /// Attaches Google Calendar account to Schedule Item
        /// </summary>
        public void ConnectGoogle()
        {
            log.Info("Call for Base:ConnectGoogle()");
            try
            {
                GoogleCalendar = new GoogleCalendarAPI();
            }
            catch (Exception ex)
            {
                log.Debug("Error while creating new GoogleCalanderAPI", ex);
                throw;
            }
        }

    }
}

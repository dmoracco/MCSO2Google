using System;
using System.Collections.Generic;
using System.Text;

namespace MCSO.Scheduling.ScheduleBase.Data
{
    /// <summary>
    /// Data representing a MCSO employee.
    /// </summary>
    public class Employee
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Name of employee.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Employee ID number.
        /// </summary>
        public int EmployeeID { get; }
        /// <summary>
        /// ID of subcalendar on associated Schedule's Google Calendar
        /// </summary>
        public string SubCalendarID { get; set; }

        public Employee(string name, int id)
        {
            log.Info("Creating new Employee");
            Name = name;
            EmployeeID = id;
            SubCalendarID = "";
        }
	}
}

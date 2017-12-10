using System;
using System.Collections.Generic;
using System.Text;

namespace MCSO.Scheduling.ScheduleBase.Data
{
    /// <summary>
    /// Contains employee information.
    /// </summary>
    public class Employee
    {
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
            Name = name;
            EmployeeID = id;
            SubCalendarID = "";
        }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MCSO.Scheduling.ScheduleBase.Data
{
    /// <summary>
    /// Data representing a assigned work shift.
    /// </summary>
    public class Shift
    {
        /// <summary>
        /// Designates shift corisponding to shift legend
        /// </summary>
        public string ShiftDesignation { get; }
        /// <summary>
        /// Starting Date and Time of shift
        /// </summary>
        public DateTime StartDateTime { get; }
        /// <summary>
        /// Ending Date and Time of shift
        /// </summary>
        public DateTime EndDateTime { get; }
        /// <summary>
        /// Starting Date of shift
        /// </summary>
        public DateTime Date
        {
            get { return StartDateTime.Date; }
        }
        /// <summary>
        /// Employee assigned to shift
        /// </summary>
        public Employee Employee { get; }
        /// <summary>
        /// Returns the Date that begins the week (Sunday)
        /// </summary>
        public DateTime PartOfWeek
        {
            get
            {
                // Return if already Sunday
                if ((int)StartDateTime.DayOfWeek == 0)
                {
                    return StartDateTime.Date;
                }
                // Calculate and return Date for previous Sunday
                else
                {
                    int days = (int)StartDateTime.DayOfWeek;
                    DateTime startofweek = StartDateTime.AddDays(-days);
                    return startofweek.Date;
                }
            }
        }
        public int ControlNumber { get; }
 
        public Shift(Employee employee, DateTime starttime, DateTime endtime, string shiftdesignation, int controlnumber)
        {
            try
            {

                // Set and validate Start and End Date/Times
                StartDateTime = starttime;
                if (DateTime.Compare(endtime, starttime) > 0)
                {
                    EndDateTime = endtime;
                }
                else
                {
                    throw new Exception("Start Date/Time later or the same as End Date/Time");
                }

                // Assign Employee and other variables
                ShiftDesignation = shiftdesignation;
                Employee = employee;
                ControlNumber = controlnumber;
            }
            catch
            {
                throw;
            }
        }

        
        
	}
}

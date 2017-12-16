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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Designates shift corisponding to shift legend
        /// </summary>
        public string ShiftDesignation { get; set; }
        /// <summary>
        /// Starting Date and Time of shift
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// Ending Date and Time of shift
        /// </summary>
        public DateTime EndDateTime { get; set; }
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
        public Employee Employee { get; set; }
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
        public int ControlNumber { get; set; }

        public Shift()
        {
            //log.Info("Creating new Shift");
            //try
            //{

            //    // Set and validate Start and End Date/Times
            //    StartDateTime = starttime;
            //    if (DateTime.Compare(endtime, starttime) > 0 || shiftdesignation == "Vacation" )
            //    {
            //        if (shiftdesignation == "Vacation")
            //        {
            //            endtime = endtime.AddDays(1);
            //        }
            //        EndDateTime = endtime;
            //    }
            //    else
            //    { 
            //        throw new Exception("End Date/Time earlier than Start Date/Time");
            //    }

            //    // Assign Employee and other variables
            //    ShiftDesignation = shiftdesignation;
            //    Employee = employee;
            //    ControlNumber = controlnumber;
            //}
            //catch (Exception ex)
            //{
            //    log.Debug("Error while validating data of new Shift", ex);
            //    throw;
            //}
        }

        
        
	}
}

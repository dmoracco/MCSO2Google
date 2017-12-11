using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSO.Scheduling.ScheduleBase
{
    /// <summary>
    /// Handles collection of assigned work Shifts for a given date.
    /// </summary>
    public class WorkDay: CalendarBaseScheduleItem
	{
        /// <summary>
        /// Date of the work day.
        /// </summary>
        public DateTime Date { get; }
        /// <summary>
        /// Collection of shifts for this work day.
        /// </summary>
        public List<Shift> ShiftList { get; }
        /// <summary>
        /// Returns the Date of the begining of the week this work day is in. (Sunday)
        /// </summary>
        public DateTime PartOfWeek { get; }
        public WorkDay(Shift initialshift)
        {
            ShiftList = new List<Shift>();
            Date = initialshift.Date;
            AddShift(initialshift);            
            PartOfWeek = initialshift.PartOfWeek;
        }

        public override void AddShift(Shift newshift)
        {
            try
            {
                // Validate dates
                if (newshift.Date == this.Date)
                {
                    ShiftList.Add(newshift);
                }
                else
                {
                    string ex = String.Format("Attempt to add new Shift with date {0} to incorrect workday {1}.", newshift.Date, this.Date);
                    throw new Exception(ex);
                }
            }
            catch
            {
                throw;
            }
                
        }
                
            
            
    }

}
	


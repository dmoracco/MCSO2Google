using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSO.Scheduling.ScheduleBase
{
    /// <summary>
    /// Handles the collection and sorting of WorkDays.
    /// </summary>
    public class WorkWeek : CalendarBaseScheduleItem
	{
		
        public List<WorkDay> WorkDayList { get; }

 

        /// <summary>
        /// Represents the work week as a collection of workdays.
        /// </summary>
        /// <param name="initialshift">Creating WorkWeek with inital Shift will automatically create corrisponding WorkDay</param>
        public WorkWeek(Shift initialshift):this(new WorkDay(initialshift))
        {

        }
        public WorkWeek(WorkDay workday)
        {
            if (WorkDayList == null)
            {
                WorkDayList = new List<WorkDay>();
                StartDate = workday.PartOfWeek;
                EndDate = StartDate.AddDays(7);
            }

            try
            {
                if (workday.PartOfWeek == this.StartDate)
                {
                    WorkDayList.Add(workday);
                }
                else
                {
                    string ex = String.Format("Attempt to add new WorkDay with week date {0} to incorrect WorkWeek {1}.", 
                        workday.PartOfWeek, this.StartDate);
                    throw new Exception(ex);
                }
            }
            catch
            {
                throw;
            }
            
        }
       
        public override void AddShift(Shift newshift)
		{
            if (WorkDayList.Exists(x => x.Date == newshift.Date))
            {
                WorkDay existingday = WorkDayList.Find(y => y.Date == newshift.Date);
                existingday.AddShift(newshift);
            }
            else
            {
                WorkDayList.Add(new WorkDay(newshift));
            }
		}

	}
}

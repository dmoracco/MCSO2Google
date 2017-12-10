using Google.Apis.Calendar.v3.Data;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSO.Scheduling.ScheduleBase
{
    /// <summary>
    /// Collection of Shifts organized by collection of WorkWeeks.
    /// </summary>
    public class Schedule : CalendarBaseScheduleItem
	{
        /// <summary>
        /// List of Employees attached to current Schedule.
        /// </summary>
        public List<Employee> EmployeeList { get; }
        /// <summary>
        /// Collection of WorkWeeks within Schedule time frame.
        /// </summary>
        public List<WorkWeek> WorkWeekList { get; }

        public Schedule()
		{
            EmployeeList = new List<Employee>();
            WorkWeekList = new List<WorkWeek>(); 
            //INIT GOOGLECALANDER HERE
		}
        /// <summary>
        /// Adds shifts and automatically organizes into a WorkDay and WorkWeek.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="shiftdesignation"></param>
       
        public override void AddShift(Shift newshift)
        {
            //CAN I ABSTRACT THIS AND WORKWEEK???

            // Check if relevant WorkWeek already exists.
            if (WorkWeekList.Exists(x => x.StartDate == newshift.PartOfWeek))
            {
                WorkWeekList.Find(x => x.StartDate == newshift.PartOfWeek).AddShift(newshift);
                if (DateTime.Compare(newshift.Date, StartDate) < 0) //Duplecate code here....... wat do?
                {
                    StartDate = newshift.Date;
                }
                if (DateTime.Compare(newshift.EndDateTime.Date, EndDate) > 0)
                {
                    EndDate = newshift.EndDateTime.Date;
                }
            }
            // Else, create new WorkWeek.
            else
            {
                WorkWeekList.Add(new WorkWeek(newshift));
                
                // Update Schedule's DateTimes
                if (DateTime.Compare(newshift.Date, StartDate) < 0)
                {
                    StartDate = newshift.Date;
                }
                if (DateTime.Compare(newshift.EndDateTime.Date, EndDate) > 0)
                {
                    EndDate = newshift.EndDateTime.Date;
                }
            }
        }

        public void UploadCalendar()
        {

            foreach (WorkWeek week in _workWeeks)
            {
                foreach (WorkDay day in week._workDays)
                {
                    foreach (Shift shift in day._shifts)
                    {
                        Event shiftevent = shift.CreateCalendarEvent();
                        if (shift._employee._subCalendarID == "")
                        {
                            string newsummary = shift._employee.Name; /*+ " " + shift._employee.EmployeeID;*/
                            string newID = _cloudCalendar.FindCalendarID(newsummary);
                            if (newID == "")
                            {
                                Console.WriteLine("Adding new sub calendar");
                                newID = _cloudCalendar.AddCalendar(newsummary, "America/Chicago");
                                shift._employee._subCalendarID = newID;
                            }
                            else
                            {
                                shift._employee._subCalendarID = newID;
                            }
                        }
                        string subcalID = shift._employee._subCalendarID;
                        if (!_cloudCalendar.EventExist(shiftevent, subcalID))
                        {
                            _cloudCalendar.AddEvent(shiftevent, subcalID);
                        }

                    }
                }
            }
        }
    }
}

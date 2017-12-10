using Google.Apis.Calendar.v3.Data;
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

        public Schedule(string path)
		{
            EmployeeList = new List<Employee>();
            WorkWeekList = new List<WorkWeek>();            
		}
        /// <summary>
        /// Adds shifts and automatically organizes into a WorkDay and WorkWeek.
        /// </summary>
        /// <param name="shiftbuffer"></param>
        public override void AddShift(int employeenumber, DateTime starttime, DateTime endtime, string shiftdesignation)
        {
            if (_workWeeks.Exists(x => x.WeekStart == shiftbuffer.PartOfWeek()))
            {
                _workWeeks.Find(x => x.WeekStart == shiftbuffer.PartOfWeek()).AddShift(shiftbuffer);
            }
            else
            {
                _workWeeks.Add(new WorkWeek(shiftbuffer));
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

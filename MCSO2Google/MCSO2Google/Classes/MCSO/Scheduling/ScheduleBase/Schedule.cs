using Google.Apis.Calendar.v3.Data;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSO.Scheduling.ScheduleBase
{
    /// <summary>
    /// Handles the collection and sorting of WorkWeeks.
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

        private int _shift_control_number;

        public Schedule()
		{
            EmployeeList = new List<Employee>();
            WorkWeekList = new List<WorkWeek>();
            ConnectGoogle();
            _shift_control_number = 0;
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
            _shift_control_number++;

            // Check if relevant WorkWeek already exists.
            if (WorkWeekList.Exists(x => x.StartDate == newshift.PartOfWeek))
            {
                Console.WriteLine("Adding to existing week {0}", newshift.PartOfWeek);

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
                Console.WriteLine("Adding new week {0}", newshift.PartOfWeek);
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

        public void AddShift(Employee employee, DateTime starttime, DateTime endtime, string shiftdesignation)
        {
            AddShift(employee, starttime, endtime, shiftdesignation, this._shift_control_number);
        }

        public void UploadSchedule()
        {

            foreach (WorkWeek week in WorkWeekList)
            {
                foreach (WorkDay day in week.WorkDayList)
                {
                    foreach (Shift shift in day.ShiftList)
                    {
                        try
                        {
                            var shiftentry = new CalendarShiftEntry(shift);
                            GoogleCalendar.AddEvent(shiftentry);
                        }
                        catch
                        {
                            throw;
                        }
                        

                    }
                }
            }
        }

        public List<Shift> ShiftList()
        {
            var shiftlist = new List<Shift>();

            foreach (WorkWeek week in WorkWeekList)
            {
                foreach (WorkDay day in week.WorkDayList)
                {
                    foreach (Shift shift in day.ShiftList)
                    {
                        shiftlist.Add(shift);
                    }
                }
            }

            return shiftlist;
        }
        /// <summary>
        /// Removes shift from collections. Removes collections if empty.
        /// </summary>
        /// <param name="controlnumber">ControlNumber of Shift</param>
        public void RemoveShift(int controlnumber)
        {
            foreach (WorkWeek week in WorkWeekList.ToArray())
            {
                foreach (WorkDay day in week.WorkDayList.ToArray())
                {
                    foreach (Shift shift in day.ShiftList.ToArray())
                    {
                        if (shift.ControlNumber == controlnumber)
                        {
                            day.ShiftList.Remove(shift);
                            if (day.ShiftList.Count < 1)
                            {
                                week.WorkDayList.Remove(day);
                                if (week.WorkDayList.Count < 1)
                                {
                                    WorkWeekList.Remove(week);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

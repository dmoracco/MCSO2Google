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

        public CSV.CSVFile CSVFile
        {
            get => default(CSV.CSVFile);
            set
            {
            }
        }

        public Forms.MainForm MainForm
        {
            get => default(Forms.MainForm);
            set
            {
            }
        }

        public CalendarShiftEntry CalendarShiftEntry
        {
            get => default(CalendarShiftEntry);
            set
            {
            }
        }

        private int _shift_control_number;

        public Schedule()
		{
            log.Info("Creating new Schedule");
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
            log.Info("Call to Schedule::AddShift(Shift)");
            _shift_control_number++;

            // Check if relevant WorkWeek already exists.
            if (WorkWeekList.Exists(x => x.StartDate == newshift.PartOfWeek))
            {
                string info = String.Format("Adding to existing week {0}", newshift.PartOfWeek);
                log.Info(info);

                WorkWeekList.Find(x => x.StartDate == newshift.PartOfWeek).AddShift(newshift);
                UpdateScheduleDates(newshift);
            }
            // Else, create new WorkWeek.
            else
            {
                string info = String.Format("Adding new week {0}", newshift.PartOfWeek);
                log.Info(info);

                WorkWeekList.Add(new WorkWeek(newshift));
                UpdateScheduleDates(newshift);
            }
        }

        public void AddShift(Employee employee, DateTime starttime, DateTime endtime, string shiftdesignation)
        {
            log.Info("Call for Schedule::AddShift(Employee, DateTime, DateTime, string");
            AddShift(employee, starttime, endtime, shiftdesignation, this._shift_control_number);
        }

        public void UploadSchedule()
        {
            log.Info("Call for Schedule::UploadSchedule");

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
                        catch (Exception ex)
                        {
                            log.Debug("Error while uploading CalanderShiftEntry to Google Calander", ex);
                        }
                    }
                }
            }
        }

        public List<Shift> ShiftList()
        {
            log.Info("Call for Schedule::ShiftList");
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
            log.Info("Call for Schedule::RemoveShift");
            foreach (WorkWeek week in WorkWeekList.ToArray())
            {
                foreach (WorkDay day in week.WorkDayList.ToArray())
                {
                    foreach (Shift shift in day.ShiftList.ToArray())
                    {
                        try
                        {
                            if (shift.ControlNumber == controlnumber)
                            {
                                day.ShiftList.Remove(shift);
                                log.Info("Shift found... Removing");

                                if (day.ShiftList.Count < 1)
                                {
                                    log.Info("Removing associated WorkDay");
                                    week.WorkDayList.Remove(day);
                                    if (week.WorkDayList.Count < 1)
                                    {
                                        log.Info("Removing associated WorkWeek");
                                        WorkWeekList.Remove(week);
                                    }
                                }
                            }
                            

                        }
                        catch (Exception ex)
                        {
                            log.Debug("Error while removing shift and/or associated Schedule Collections", ex);
                        }
                        
                    }
                }
            }
        }
        private void UpdateScheduleDates(Shift shift)
        {
            log.Info("Call for Scheudle UpdateScheduleDates");
            if (DateTime.Compare(shift.Date, StartDate) < 0)
            {
                StartDate = shift.Date;
            }
            if (DateTime.Compare(shift.EndDateTime.Date, EndDate) > 0)
            {
                EndDate = shift.EndDateTime.Date;
            }
        }
    }
}

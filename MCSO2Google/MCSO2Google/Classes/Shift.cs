using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class Shift
    {
        private char _designation;
        private DateTime _start;
        private DateTime _end;
        private bool _partialShift;
        internal Employee _employee;
        public DateTime StartDateTime { get { return _start; } }
        public DateTime EndDateTime { get { return _end; } }
        public DateTime ShiftDate { get {return _start.Date;} }
        public string ShiftDesignation { get { return _designation.ToString(); } }

        public Shift(DateTime start, DateTime end, char shiftID, Employee employee)
        {
            //validate these DateTime inputs
            _start = start;
            _end = end;
            _designation = shiftID;
            _partialShift = false;
            _employee = employee;
        }

        public DateTime PartOfWeek()
        {
            if ((int)this._start.DayOfWeek == 0)
                return _start.Date;
            else
            {
                int days = (int)this._start.DayOfWeek;
                DateTime startofweek = this._start.AddDays(-days);
                return startofweek.Date;
            }
        }
        public Event CreateCalendarEvent()
        {
            Event newshift = new Event()
            {
                //Id = _employee.EmployeeID + _start.ToString() + _end.ToString(),
                Summary = _designation + " Shift",
                Location = "1234 5th Ave SE, Bismarck, ND 58503",
                Start = new EventDateTime()
                {
                    DateTime = _start,
                    TimeZone = "America/Chicago",

                },
                End = new EventDateTime()
                {
                    DateTime = _end,
                    TimeZone = "America/Chicago",
                }
                
            };
            return newshift;

        }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class Employee
    {
        private string _name;
        private int _employeeID;
        private int _minHours;
        private double _hoursWorked;
        private string _roll;
        public int VacationHours;
        internal string _subCalendarID;

        public int EmployeeID {get{return _employeeID; } }
        public string Name { get { return _name; } }

        public Employee(string name, int id)
        {
            _name = name;
            _employeeID = id;
            _minHours = 40;
            _hoursWorked = 0;
            _subCalendarID = "";
        }
	}
}

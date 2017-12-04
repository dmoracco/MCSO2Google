using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public class Schedule : ScheduleCalendar
	{
        public List<Employee> _employees;
        public List<WorkWeek> _workWeeks;
        public Schedule(string path)
		{
            _employees = new List<Employee>();
            _workWeeks = new List<WorkWeek>();
            CSVFile csv = new CSVFile(path);
            string currentline = csv.GetNextLine();
            bool test = false;


            while (test == false)
            {

                string[] segments = currentline.Split(',');
                for (int i = 0; i < segments.Length; i++)
                    segments[i] = segments[i].Replace('"', ' ').Trim();
                // Utilize Filter here? defaulting to dispatch's format...

                int intbuffer = Int32.Parse(segments[0]);
                
                if (!_employees.Exists(x => x.EmployeeID == intbuffer))
                {
                    if (_employees.Exists(x => x.Name == segments[1]))
                    {
                        //pop up warning of existing names but different ID's, confirmation to continue
                    }
                    _employees.Add(new Employee(segments[1], intbuffer));
                }
 
                Employee employeebuffer = _employees.Find(x => x.EmployeeID == intbuffer);


                DateTime startbuffer = DateTime.Parse(segments[7]);
                DateTime endbuffer = DateTime.Parse(segments[8]);
                char[] letterbuffer = segments[6].ToCharArray();

                Shift shiftbuffer = new Shift(startbuffer, endbuffer, letterbuffer[0], employeebuffer);
                
                //validate unique Shift before adding it to WD, WW. IEquitable?

                //try catch here?
                if (_workWeeks.Exists(x => x.WeekStart == shiftbuffer.PartOfWeek()))
                {
                    _workWeeks.Find(x => x.WeekStart == shiftbuffer.PartOfWeek()).AddShift(shiftbuffer);
                }
                else
                {
                    _workWeeks.Add(new WorkWeek(shiftbuffer));
                }
                currentline = csv.GetNextLine();
                if (currentline == null)
                    test = true;
                
            }
            
		}
        public void ConnectGoogle()
        {
            _cloudCalendar = new GoogleCalendarAPI();
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
                            Console.WriteLine("Adding new sub calendar");
                            string summarybuffer = shift._employee.Name + " " + shift._employee.EmployeeID;
                            string newID = _cloudCalendar.AddCalendar(summarybuffer, "America/Chicago");
                            shift._employee._subCalendarID = newID;                            
                        }

                        string subcalID = shift._employee._subCalendarID;

                        _cloudCalendar.AddEvent(shiftevent, subcalID);
                    }
                }
            }
        }
	}
}

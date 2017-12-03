using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public class Schedule : Calendar
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
                
                DateTime startbuffer = DateTime.Parse(segments[7]);
                DateTime endbuffer = DateTime.Parse(segments[8]);
                char[] letterbuffer = segments[5].ToCharArray();

                Shift shiftbuffer = new Shift(startbuffer, endbuffer, letterbuffer[0]);

                Console.WriteLine("PartOfWeek: {0}", shiftbuffer.PartOfWeek());
                
                //validate unique Shift before adding it to WD, WW. IEquitable?

                //try catch here?
                if (_workWeeks.Exists(x => x.WeekStart == shiftbuffer.PartOfWeek()))
                {
                    Console.WriteLine("Adding to existing week: {0}", _workWeeks.Find(x => x.WeekStart == shiftbuffer.PartOfWeek()).WeekStart);
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
	}
}

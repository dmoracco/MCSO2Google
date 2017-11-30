using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public class Schedule : Calendar
	{
        private List<Employee> _employees;
        private List<WorkWeek> _workWeeks;

		public void ImportCSV(string path)
		{
            CSVFile csv = new CSVFile(path);
            string currentline;
            

            while ((currentline = csv.GetNextLine()) != null)
            {
                string[] segments = currentline.Split(',');
                // Utilize Filter here? defaulting to dispatch's format...

                int intbuffer = Int32.Parse(segments[0]);

                if (!_employees.Exists(x => x.EmployeeID != intbuffer))
                {
                    if (_employees.Exists(x => x.Name == segments[1]))
                    {
                        //pop up warning of existing names but different ID's, confirmation to continue
                    }
                    _employees.Add(new Employee(segments[1], intbuffer));
                }

                DateTime startbuffer = DateTime.Parse(segments[6]);
                DateTime endbuffer = DateTime.Parse(segments[7]);
                char[] letterbuffer = segments[5].ToCharArray();

                Shift shiftbuffer = new Shift(startbuffer, endbuffer, letterbuffer[0]);
                //validate unique Shift before adding it to WD, WW. IEquitable?
                if (_workWeeks.Exists(x => x.))
            }
            
		}
	}
}

using MCSO.Scheduling.ScheduleBase;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MCSO.Scheduling.CSV.Input
{
    /// <summary>
    /// Parses CSV file by lines.
    /// </summary>

    public class CSVFile
    {
        /// <summary>
        /// List of string entries of CSV file parsed by lines
        /// </summary>
        public List<string> EntryList { get; }
     

        public CSVFile(string path)
        {
            // Initialization
            string line;
            EntryList = new List<string>();

            // Open CSV File
            try
            {
                using (StreamReader CSVFile = File.OpenText(path))
                {
                    // Check if properly formated Scheduling CSV File
                    line = CSVFile.ReadLine();
                    if (!line.Contains("ExpRefNum"))
                    {
                        throw new Exception("Selected file does not conform to MCSO Scheduler standard.");
                    }
                    // Populate EntryList
                    while ((line = CSVFile.ReadLine()) != null)
                    {
                        EntryList.Add(line);
                    }
                }
            }
            catch
            {
                throw;
            }


        }

        /// <summary>
        /// Populate Schedule by parsing current CSV File
        /// </summary>
        /// <param name="schedule">Schedule object to populate</param>
        public void PopulateSchedule(Schedule schedule)
        {            
            string[] segments;
            foreach (string line in EntryList)
            {
                try
                {
                    // Separate values and clean output
                    segments = line.Split(',');
                    foreach (string part in segments)
                    {
                        part.Replace('"', ' ').Trim();
                    }

                    // Check employee existance, create if needed
                    int employeenumber = Int32.Parse(segments[0]);
                    string employeename = segments[1];
                    var employee = schedule.EmployeeList.Find(x => x.EmployeeID == employeenumber);
                    if (employee == null)
                    {
                        employee = new Employee(employeename, employeenumber);
                    }

                    // Retreive other values
                    string shiftdesignation = segments[6];
                    DateTime starttime = DateTime.Parse(segments[7]);
                    DateTime endtime = DateTime.Parse(segments[8]);

                    // Populate Schedule
                    schedule.AddShift(employee, starttime, endtime, shiftdesignation);
                }
                catch
                {
                    //ERROR LOGGING, BUT CONTINUE
                }
               
                
            }           

        }

    }
}
    

using MCSO.Scheduling.ScheduleBase;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MCSO.Scheduling.CSV
{
    
    /// <summary>
    /// Handles the parsing of MCSO Schedule Comma Separated Value files.
    /// </summary>
    public class CSVFile
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// List of string entries of CSV file parsed by lines
        /// </summary>
        public List<string> EntryList { get; }

        public Forms.MainForm MainForm
        {
            get => default(Forms.MainForm);
            set
            {
            }
        }

        public CSVFile(string path)
        {
            // Initialization            
            log.Info("Creating CSVFile object");
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
            catch (Exception ex)
            {
                log.Debug("Error while attempting to open CSV file: {0}" , ex);
                throw;
            }


        }

        /// <summary>
        /// Populate Schedule by parsing current CSV File
        /// </summary>
        /// <param name="schedule">Schedule object to populate</param>
        public void PopulateSchedule(Schedule schedule)
        {
            log.Info("Call for CSVFile::PopulateSchedule");
            string[] segments;
            foreach (string line in EntryList)
            {
                try
                {
                    // Separate values and clean output
                    segments = line.Split(',');
                    for (int i = 0; i < segments.Length; i++)
                    {
                        segments[i] = segments[i].Replace("\"", " ").Trim();
                    }

                    // Check employee existance, create if needed
                    int employeenumber = Int32.Parse(segments[0]);
                    string employeename = segments[1];
                    var employee = schedule.EmployeeList.Find(x => x.EmployeeID == employeenumber);
                    if (employee == null)
                    {
                        employee = new Employee(employeename, employeenumber);
                        schedule.EmployeeList.Add(employee);
                    }

                    // Retreive other values
                    string shiftdesignation = segments[6];
                    DateTime starttime = DateTime.Parse(segments[7]);
                    DateTime endtime = DateTime.Parse(segments[8]);

                    // Populate Schedule
                    schedule.AddShift(employee, starttime, endtime, shiftdesignation);
                }
                catch (Exception ex)
                {
                    string errmsg = String.Format("Error while populating line:\n {0} \n with error {1}... Continueing", line, ex);
                    log.Debug(errmsg);
                }
               
                
            }           

        }

    }
}
    

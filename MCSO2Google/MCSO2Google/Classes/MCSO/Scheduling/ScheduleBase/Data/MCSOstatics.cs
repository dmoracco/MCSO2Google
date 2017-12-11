using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSO.Scheduling.ScheduleBase.Data
{
    /// <summary>
    /// MCSO Statics and Definitions
    /// </summary>
    public class MCSOstatics
    {
        public const string ApplicationName = "MCSO Scheduling Assistant";
        public const string Name = "Morton County Sheriff's Office";
        public const string Address = "205 1st Ave NW, Mandan, ND 58554";
        public const string IANATimeZone = "America/Chicago";
        public static string[] DispatchLegend = { "A", "B", "C", "D", "E", "F", "G", "I", "X", "FL", "AD", "T", "V" };

        public GoogleAPI.GoogleCalendarAPI GoogleCalendarAPI
        {
            get => default(GoogleAPI.GoogleCalendarAPI);
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
    }
}

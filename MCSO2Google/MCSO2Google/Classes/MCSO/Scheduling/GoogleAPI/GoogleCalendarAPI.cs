using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCSO.Scheduling.GoogleAPI
{
    /// <summary>
    /// Handles interaction between MCSO Schedule data and Google Calendar API.
    /// </summary>
    public class GoogleCalendarAPI
	{
        private UserCredential _credential;
        private CalendarService _service;
        private string _credPath;
        private static string[] _scopes = { CalendarService.Scope.Calendar};
        private const string _applicationName = MCSOstatics.ApplicationName;
        
        public GoogleCalendarAPI()
        {
            // Look for client_secret API key and create credential token
            using (var stream =
               new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                _credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                _credPath = Path.Combine(_credPath, ".MCSOcredentials/MCSOcalendar.json");

                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(_credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + _credPath);
            }

            // Create Google Calendar API service.
            _service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });
        }

        /// <summary>
        /// Searches CalendarList for matching subcalendar and returns its ID.
        /// </summary>
        /// <param name="name">Employee name</param>
        /// <returns>Subcalendar ID</returns>
        public string FindCalendarID(string name)
        {
            // Get Calendar List
            CalendarList calendarList = _service.CalendarList.List().Execute();

            // Look for matching calendar
            foreach (CalendarListEntry entry in calendarList.Items)
            {
                string summary = entry.Summary;
                if (summary == name)
                {
                    return entry.Id;
                }
            }

            return "";
        }

        /// <summary>
        /// Checks if ShiftEntry already exists.
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="calendarID"></param>
        /// <returns></returns>
        public bool EventExist(CalendarShiftEntry shiftentry)
        {
            // Build Events request
            EventsResource.ListRequest request = _service.Events.List(shiftentry.SubCalendarID);
            request.TimeMin = shiftentry.ShiftEvent.Start.DateTime;
            request.TimeMax = shiftentry.ShiftEvent.End.DateTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Send request and parse Events response
            Events events = request.Execute();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    if (eventItem.Summary == shiftentry.ShiftEvent.Summary)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Adds new SubCalendar to current "primary" Calendar.
        /// </summary>
        /// <param name="summary"></param>
        /// <returns></returns>
        public string AddCalendar(string summary)
        {
            // Create new calendar to upload
            Calendar newcalendar = new Calendar()
            {
                Summary = summary,
                TimeZone = MCSOstatics.IANATimeZone
            };

            // Upload
            Calendar createdCalendar = _service.Calendars.Insert(newcalendar).Execute();

            // Returns uploaded calendar's ID
            return createdCalendar.Id;
        }

        /// <summary>
        /// Adds event to appropriate subcalendar. Creates subcalendar if it doesn't exist.
        /// </summary>
        /// <param name="shiftentry"></param>
        public void AddEvent(CalendarShiftEntry shiftentry)
        { 
            if (shiftentry.SubCalendarID == "")
            {
                string calenderID = FindCalendarID(shiftentry.EmployeeName);
                if (calenderID == "")
                {
                    calenderID = AddCalendar(shiftentry.EmployeeName);
                }
                shiftentry.SubCalendarID = calenderID;
            }

            if (!EventExist(shiftentry))
            {
                EventsResource.InsertRequest request = _service.Events.Insert(shiftentry.ShiftEvent, shiftentry.SubCalendarID);
                Event createdEvent = request.Execute();
            }
        }

        /// <summary>
        /// Retrieves primary Google Calendar account ID (email address).
        /// </summary>
        /// <returns>Email address of primary Google Calendar account</returns>
        public string GetAccountEmail()
        {

            var primeCalendar = _service.Calendars.Get("primary").Execute();
            string email = primeCalendar.Id;
            return email;
        }

        /// <summary>
        /// Used to remove current accounts credentials
        /// </summary>
        public void ClearCredentials()
        {
            if (Directory.Exists(_credPath))
            {
                foreach (var file in Directory.GetFiles(_credPath))
                {
                    File.Delete(file);
                }
                Directory.Delete(_credPath);

            }

        }
        
    }
}

using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler
{
	public class GoogleCalendarAPI
	{
        private UserCredential _credential;
        private CalendarList _calendarList;
        private static string[] Scopes = { CalendarService.Scope.Calendar};
        private static string ApplicationName = "MCSO Schedule Assistant";
        private CalendarService _service;
        private string _credPath;

        public GoogleCalendarAPI()
        {
            using (var stream =
               new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                _credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                _credPath = Path.Combine(_credPath, ".MCSOcredentials/calendar-dotnet-quickstart.json");

                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(_credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + _credPath);
            }

            // Create Google Calendar API service.
            this._service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = ApplicationName,
            });
        }
        public string FindCalendarID(string name)
        {
            CalendarList calendarList = _service.CalendarList.List().Execute();
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

        public bool EventExist(Event shift, string calendarID)
        {
            EventsResource.ListRequest request = _service.Events.List(calendarID);
            request.TimeMin = shift.Start.DateTime;
            request.TimeMax = shift.End.DateTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    if (eventItem.Summary == shift.Summary)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public List<string> ListEvents(string calendarID)
        {
            List<string> eventslist = new List<String>();

            EventsResource.ListRequest request = _service.Events.List(calendarID);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            Console.WriteLine(events.Items.Count);
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    string buffer = eventItem.Summary + " " + when;
                    eventslist.Add(buffer);
                }
            }

            return eventslist;
        }
        public string AddCalendar(string summary, string timezone)
        {
            Calendar calendar = new Calendar();
            calendar.Summary = summary;
            calendar.TimeZone = timezone;

            Calendar createdCalendar = _service.Calendars.Insert(calendar).Execute();

            return createdCalendar.Id;
        }

        public void AddEvent(Event newevent, string calendarID)
        {
            EventsResource.InsertRequest request = _service.Events.Insert(newevent, calendarID);
            Event createdEvent = request.Execute();
        }

        public string GetAccountEmail()
        {

            var primeCalendar = _service.Calendars.Get("primary").Execute();
            string email = primeCalendar.Id;
            return email;
        }

        public void ClearAccount()
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
        public Event CreateCalendarEvent() // FROM SHIFT!
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

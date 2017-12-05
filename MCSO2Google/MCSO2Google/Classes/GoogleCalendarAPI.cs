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

        public GoogleCalendarAPI()
        {
            using (var stream =
               new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            this._service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = ApplicationName,
            });
        }
        public List<string> ListEvents(string calendarID)
        {
            List<string> eventslist = new List<String>();

            EventsResource.ListRequest request = _service.Events.List(calendarID);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
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

	}
}

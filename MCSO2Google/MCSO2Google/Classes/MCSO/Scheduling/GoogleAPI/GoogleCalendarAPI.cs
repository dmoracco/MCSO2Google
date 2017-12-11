using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MCSO.Scheduling.ScheduleBase.Data;
using System;
using System.IO;
using System.Threading;

namespace MCSO.Scheduling.GoogleAPI
{
    /// <summary>
    /// Handles interaction between MCSO Schedule data and Google Calendar API.
    /// </summary>
    public class GoogleCalendarAPI
	{
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private UserCredential _credential;
        private CalendarService _service;
        private string _credPath;
        private static string[] _scopes = { CalendarService.Scope.Calendar};
        private const string _applicationName = MCSOstatics.ApplicationName;
        
        public GoogleCalendarAPI()
        {
            log.Info("Creating new GoogleCalendarAPI");
            // Look for client_secret API key and create credential token
            try
            {
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
            }
            catch (Exception ex)
            {
                log.Debug("Error while creating Google API credentials: {0}", ex);
                throw;
            }


            // Create Google Calendar API service.
            _service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });
        }

        public ScheduleBase.Schedule Schedule
        {
            get => default(ScheduleBase.Schedule);
            set
            {
            }
        }

        /// <summary>
        /// Searches CalendarList for matching subcalendar and returns its ID.
        /// </summary>
        /// <param name="name">Employee name</param>
        /// <returns>Subcalendar ID</returns>
        public string FindCalendarID(string name)
        {
            log.Info("Call for GoogleCalendarAPI::FindcalendarID");
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
            log.Info("Call for GoogleCalendarAPI::EventExist");
            // Build Events request
            EventsResource.ListRequest request = _service.Events.List(shiftentry.SubCalendarID);
            request.TimeMin = shiftentry.ShiftEvent.Start.DateTime;
            request.TimeMax = shiftentry.ShiftEvent.End.DateTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Send request and parse Events response
            try
            {
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
            }
            catch (Exception ex)
            {
                log.Debug("Error while requesting Google Calander Event", ex);
                throw;
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
            log.Info("Call for GoogleCalanderAPI::AddCalendar");
            // Create new calendar to upload
            Calendar newcalendar = new Calendar()
            {
                Summary = summary,
                TimeZone = MCSOstatics.IANATimeZone
            };

            // Upload
            try
            {
                Calendar createdCalendar = _service.Calendars.Insert(newcalendar).Execute();
                // Returns uploaded calendar's ID
                return createdCalendar.Id;
            }
            catch (Exception ex)
            {
                log.Debug("Error while attemping to add new SubCalendar to Google Calendar", ex);
                throw;
            }
            

            
        }

        /// <summary>
        /// Adds event to appropriate subcalendar. Creates subcalendar if it doesn't exist.
        /// </summary>
        /// <param name="shiftentry"></param>
        public void AddEvent(CalendarShiftEntry shiftentry)
        {
            log.Info("Call to GoogleCalendarAPI::AddEvent");
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
                try
                {
                    EventsResource.InsertRequest request = _service.Events.Insert(shiftentry.ShiftEvent, shiftentry.SubCalendarID);
                    Event createdEvent = request.Execute();
                    log.Info("CalandarShiftEntry successfully uploaded");
                }
                catch (Exception ex)
                {
                    log.Debug("Error while attempting to add new event to Google Calander SubCalendar", ex);
                }
            }
            else
            {
                log.Info("CalanderShiftEntry already exists... skipping");
            }
        }

        /// <summary>
        /// Retrieves primary Google Calendar account ID (email address).
        /// </summary>
        /// <returns>Email address of primary Google Calendar account</returns>
        public string GetAccountEmail()
        {
            log.Info("Call to GoogleCalanderAPI::GetAccountEmail");
            try
            {
                var primeCalendar = _service.Calendars.Get("primary").Execute();
                string email = primeCalendar.Id;
                return email;
            }
            catch (Exception ex)
            {
                log.Debug("Error while requesting \"primary\" Google Calander ID (usually email address)", ex);
                throw; 
            }
            
        }

        /// <summary>
        /// Used to remove current accounts credentials
        /// </summary>
        public void ClearCredentials()
        {
            log.Info("Call for GoogleCalanderAPI::ClearCredentials");
            try
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
            catch (Exception ex)
            {
                log.Debug("Error attempting to delete credentials folder", ex);
            }
            
        }
        
    }
}

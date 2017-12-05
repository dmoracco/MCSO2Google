using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public abstract class ScheduleCalendar
	{
		protected DateTime StartDate;
		protected DateTime EndDate;
		public GoogleCalendarAPI _cloudCalendar;
	}
}

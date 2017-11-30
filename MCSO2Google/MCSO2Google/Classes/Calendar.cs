using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
	public abstract class Calendar
	{
		protected DateTime StartDate;
		protected DateTime EndDate;
		protected GoogleCalendar _cloudCalendar;
	}
}

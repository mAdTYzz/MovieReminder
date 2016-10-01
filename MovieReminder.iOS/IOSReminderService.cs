using System;
using System.Collections.Generic;
using System.Linq;
using EventKit;
using Foundation;
using MovieReminder.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IOSReminderService))]
namespace MovieReminder.iOS
{
	public class IOSReminderService:IReminderService
	{
		#region Properties and other goodies

		public static EKEventStore EventStore
		{
			get;
			set;
		}

		public EKCalendar MovieReminderWorkCalendar
		{
			get;
			set;
		}

		private bool accessGranted = false;

		#endregion

		public IOSReminderService()
		{
			
		}

		public void StartService()
		{
			if (EventStore == null)
			{
				try
				{
					EventStore = new EKEventStore();

					EventStore.RequestAccess(EKEntityType.Event, (bool granted, NSError e) =>
					{
						if (granted == true)
						{
							accessGranted = true;
						}
					});
				}
				catch (Exception ex)
				{
					LoggingService.AddEntryToLog(ex.Message, ex.StackTrace);
				}
			}
		}

		private EKCalendar AssignMovieReminderCalendar()
		{

			if (MovieReminderWorkCalendar == null || MovieReminderWorkCalendar.Title != "MovieReminderEvents")
			{
				if (EventStore != null && EventStore.Calendars != null && EventStore.Calendars.Length > 0)
				{
					return (from x in EventStore.Calendars
							where x.Title == "MovieReminderEvents"
							select x).FirstOrDefault();
				}
				else
					return null;
			}
			else
				return MovieReminderWorkCalendar;
		}

		private NSError CalendarInit()
		{
			NSError error = null;

			if (MovieReminderWorkCalendar==null)
			{
				MovieReminderWorkCalendar = AssignMovieReminderCalendar();

				if (MovieReminderWorkCalendar == null) //Create a new calendar if calendar doesn't exist
				{
					MovieReminderWorkCalendar = EKCalendar.Create(EKEntityType.Event, EventStore);
					MovieReminderWorkCalendar.Title = "MovieReminderEvents";

					EKSource calendarSource = null;

					calendarSource = (from x in EventStore.Sources
									  where x.SourceType == EKSourceType.CalDav
									  select x).FirstOrDefault();

					MovieReminderWorkCalendar.Source = calendarSource;
					EventStore.SaveCalendar(MovieReminderWorkCalendar, true, out error);
				}
			}

			return error;
		}

		public bool AddReminder(DateTime startDateTime, string title, string body)
		{
			bool operationSuccessfull = false;

			startDateTime = startDateTime.AddHours(12); //Setting the time of the event to noon

			if (accessGranted==true)
			{
				//Add reminder code
				if (CalendarInit() == null) //If no error is thrown
				{
					if (CheckIfEventExists(title,startDateTime)==false)
					{
						NSError error = null;

						EKEvent movieEvent = EKEvent.FromStore(EventStore);

						/* For testing!!
						 movieEvent.StartDate = (NSDate)(startDateTime.AddHours(1).AddMinutes(45));
						movieEvent.EndDate = (NSDate)(startDateTime.AddHours(2).AddMinutes(45));
						movieEvent.AddAlarm(EKAlarm.FromDate((NSDate)startDateTime.AddHours(1).AddMinutes(45)));
						 */
						title += " is showing tomorrow! ";
						movieEvent.StartDate = ConvertDateTimeToNSDate(startDateTime);
						movieEvent.EndDate = ConvertDateTimeToNSDate(startDateTime.AddHours(1));

						movieEvent.AddAlarm(EKAlarm.FromDate(ConvertDateTimeToNSDate(startDateTime)));
						//movieEvent.AddAlarm(EKAlarm.FromDate((NSDate)startDateTime.AddMinutes(5)));

						movieEvent.Title = title;
						movieEvent.Notes = body;
						movieEvent.Calendar = MovieReminderWorkCalendar;
						EventStore.SaveEvent(movieEvent, EKSpan.ThisEvent, out error);

						operationSuccessfull = true; 

						new UIAlertView("Event added to calendar", "The reminder was sucessfully added to the calendar!", null, "Ok", null).Show();
					}
					else
						new UIAlertView("Existing reminder!", "The reminder allready exists in the calendar", null, "Ok", null).Show();
					}
			}
			else
			{
				new UIAlertView("Access Denied!", "User Denied Access to Calendar Data", null, "Ok", null).Show();
			}

			return operationSuccessfull;
		}

		public NSDate ConvertDateTimeToNSDate(DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
				date = DateTime.SpecifyKind(date, DateTimeKind.Local);// or DateTimeKind.Utc, this depends on each app */)
			return (NSDate)date;
		}

		public bool CheckIfEventExists(string movieTitle, DateTime eventDate)
		{
			bool eventExists = false;

			if (MovieReminderWorkCalendar!=null)
			{
				try
				{
					EKCalendar[] activeCalendars = new EKCalendar[1];
					activeCalendars[0] = MovieReminderWorkCalendar;

					NSPredicate eventQuery = EventStore.PredicateForEvents(ConvertDateTimeToNSDate(eventDate), ConvertDateTimeToNSDate(eventDate.AddHours(1))
					                                                       , activeCalendars);

					EKCalendarItem[] foundEvents = EventStore.EventsMatching(eventQuery);

					if (foundEvents != null && foundEvents.Count() > 0)
					{
						EKCalendarItem foundEvent = (from x in foundEvents
						                             where x.Notes.Contains(SearchPageViewModel.TitleHashTagConverter(movieTitle))
													 select x).FirstOrDefault(); 

						if (foundEvent != null)
						{
							eventExists = true;
						}
					}
				}
				catch (Exception ex)
				{
					LoggingService.AddEntryToLog(ex.Message, ex.StackTrace);
				}
			}

			return eventExists;
		}
	}
}

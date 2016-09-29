using System;
using MovieReminder.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(DroidReminderService))]
namespace MovieReminder.Droid
{
	public class DroidReminderService:IReminderService
	{
		public DroidReminderService()
		{
		}

		public bool AddReminder(DateTime startDateTime, string title, string body)
		{
			throw new NotImplementedException();
		}

		public void StartService()
		{
			
		}
	}
}

using System;
namespace MovieReminder
{
	public interface IReminderService
	{
		bool AddReminder(DateTime startDateTime, string title, string body);
		void StartService();
	}
}

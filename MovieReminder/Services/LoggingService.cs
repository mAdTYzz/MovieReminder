using System;
namespace MovieReminder
{
	/// <summary>
	/// Used for logging in case an exception is thrown
	/// </summary>
	public static class LoggingService
	{
		public static void AddEntryToLog(string exceptionMessage, string exceptionStackTrace)	                                
		{
			if (String.IsNullOrWhiteSpace(exceptionMessage)==false && String.IsNullOrWhiteSpace(exceptionStackTrace)==false)
			{
				//Add to log :)
			}
		}
	}
}

using System;
namespace MovieReminder_Models
{
	public static class AppConstantSettings
	{
		public const string apiKey = "cb49f7e8e3939abb086275b95e0abc15";
		public const string rootConnectURL = "http://api.themoviedb.org/3/";

		public const string omdbApiWebURI = "http://www.omdbapi.com/?t={0}&y=&plot=short&r=json&tomatoes=true"; //Defaul API connect URI
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.TMDb;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using CrossPlatformLibrary;

namespace MovieReminder_MovieDBWrap
{
	public class MovieService //Hardcoded curently to SE country code
	{
		public MovieService()
		{
		}

		public async Task<System.Net.TMDb.Movies> SearchMovie(string movieTitle)
		{
			System.Net.TMDb.Movies foundMoviesApi= new Movies();

			try
			{
				using (var tmdbClient = new ServiceClient(MovieReminder_Models.AppConstantSettings.apiKey))
				{
					foundMoviesApi = await tmdbClient.Movies.SearchAsync(movieTitle, "SE", true, null, true, 1, new CancellationToken());
				}
				return foundMoviesApi;
			}
			catch
			{				
				return null;
			}
		}

		/// <summary>
		/// Used to get countryCode
		/// </summary>
		private string GetLocation()
		{
			var geoLocator = new CrossPlatformLibrary.Geolocation.LocationService();

			geoLocator.DesiredAccuracy=30;

			var location = geoLocator.GetPositionAsync(10, null, false).Result;

			return location.ToString();
		}

		public async Task<System.Net.TMDb.Releases> UsageDemo(CancellationToken cancellationToken, string apiKey)
		{
			System.Net.TMDb.Releases releases = null;

			DateTime? releaseTheater;
			string json = null;
			using(var tmdbClient = new ServiceClient(apiKey))
			{
				List<string> response = new List<string>();	

				Movie movie = await tmdbClient.Movies.GetAsync(297761, "en", true, cancellationToken);

				releaseTheater = movie.ReleaseDate;

				foreach (var rel in movie.Releases.Results)
				{
					response.Add(rel.Date.ToString());
				}

				json = JsonConvert.SerializeObject(response);

				releases = movie.Releases;
			}

			return releases;
		}
	}
}

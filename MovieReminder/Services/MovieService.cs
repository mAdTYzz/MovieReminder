using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MovieReminder_Models;
using Prism.Mvvm;
using System.Collections.Generic;

namespace MovieReminder
{
	public class MovieService:BindableBase
	{
		private string _movieTitle;
		public string MovieTitle
		{
			get { return _movieTitle; }
			set { SetProperty(ref _movieTitle, value); }
		}

		public MovieService()
		{
		}

		private string TitleResolver()
		{
			if (string.IsNullOrWhiteSpace(MovieTitle) == false)
			{
				string resolvedTitle = string.Empty;

				foreach (char character in MovieTitle)
				{
					if (character != ' ')
					{
						resolvedTitle += character;
					}
					else
						resolvedTitle += "+";
				}

				return resolvedTitle;
			}
			else
				return null;
		}

		public Movie SearchMovie()
		{
			string webUri = String.Format(AppConstantSettings.omdbApiWebURI, TitleResolver());

			HttpClient omdbApiClient;

			using (omdbApiClient = new HttpClient())
			{
				try
				{
					string json = omdbApiClient.GetStringAsync(webUri).Result;

					Movie mv = JsonConvert.DeserializeObject<Movie>(json);

					mv.TheaterReleaseDate = SetMinimumDateIfValueNull(mv.Released);
					mv.DvdReleaseDate = SetMinimumDateIfValueNull(mv.Dvd);

					if (String.IsNullOrWhiteSpace(mv.imdbID)==false)
					{
						mv.FoundWithApi = true;

						return mv;
					}
					else
						return new Movie();
				}
				catch (Exception ex)
				{
					return null;
				}
			}
		}


		private DateTime SetMinimumDateIfValueNull(string jsonValue)
		{
			if (jsonValue != null && jsonValue != "N/A")
			{
				try
				{
					return Convert.ToDateTime(jsonValue);
				}
				catch (Exception ex)
				{
					return DateTime.MinValue;
				}
			}
			else
				return DateTime.MinValue;
		}
	}
}

/*
 * TODO:
 * If movie is not found exception is thrown
*/
using System;
using Prism.Mvvm;

namespace MovieReminder
{
	public class SearchPageViewModel : BindableBase
	{
		#region Fields and Commands

		private MovieReminder_Models.Movie _searchedMovie;
		public MovieReminder_Models.Movie SearchedMovie
		{
			get { return _searchedMovie; }
			set { SetProperty(ref _searchedMovie, value); }
		}

		#endregion

		public SearchPageViewModel()
		{
			SearchedMovie = new MovieReminder_Models.Movie()
			{
				ID = "12344",
				Title = "TestMovie",
				PosterURI = "http://cdn.collider.com/wp-content/uploads/2016/06/stranger-things-poster-netflix1.jpg",
				Theater = DateTime.Today.AddDays(10),
				Dvd = DateTime.Today.AddDays(20)
			};
		}
	}
}

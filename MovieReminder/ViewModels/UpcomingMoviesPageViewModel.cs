using System;
using System.Collections.Generic;
using Prism.Mvvm;

namespace MovieReminder
{
	public class UpcomingMoviesPageViewModel:BindableBase
	{	
		private List<MovieReminder_Models.Movie> _upcomingMoviesList;
		public List<MovieReminder_Models.Movie> UpcomingMoviesList
		{
			get { return _upcomingMoviesList; }
			set { SetProperty(ref _upcomingMoviesList, value); }
		}

		public UpcomingMoviesPageViewModel()
		{
		}

	}
}

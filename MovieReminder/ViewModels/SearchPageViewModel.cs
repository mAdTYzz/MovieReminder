using MovieReminder_Models;
using Prism.Mvvm;

namespace MovieReminder
{
	public class SearchPageViewModel : BindableBase
	{
		private MovieReminder_Models.Movie _searchedMovie;
		public MovieReminder_Models.Movie SearchedMovie
		{
			get { return _searchedMovie; }
			set { SetProperty(ref _searchedMovie, value); }
		}

		public SearchPageViewModel()
		{
			SearchedMovie = new Movie();

			SearchedMovie.Title = "ABC";
		}

	}
}

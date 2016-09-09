using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Mvvm;

namespace MovieReminder
{
	public class SearchPageViewModel : BindableBase
	{
		#region Commands and Fields

		private DelegateCommand _searchCommand;
		public DelegateCommand SearchCommand
		{
			get { return _searchCommand; }
			set { SetProperty(ref _searchCommand, value); }
		}

		private DelegateCommand _togglePlotCommand;
		public DelegateCommand TogglePlotCommand
		{
			get { return _togglePlotCommand; }
			set { SetProperty(ref _togglePlotCommand, value); }
		}

		private MovieReminder_Models.Movie _searchedMovie;
		public MovieReminder_Models.Movie SearchedMovie
		{
			get { return _searchedMovie; }
			set { SetProperty(ref _searchedMovie, value); }
		}

		private bool _isMovieFound=false;
		public bool IsMovieFound
		{
			get { return _isMovieFound; }
			set { SetProperty(ref _isMovieFound, value); }
		}

		private bool _isPlotVisible=false;
		public bool IsPlotVisible
		{
			get { return _isPlotVisible; }
			set { SetProperty(ref _isPlotVisible, value); }
		}

		private List<MovieReminder_Models.Movie> _foundMoviesList;
		public List<MovieReminder_Models.Movie> FoundMoviesList
		{
			get { return _foundMoviesList; }
			set { SetProperty(ref _foundMoviesList, value); }
		}

		MovieReminder_MovieDBWrap.MovieService mvService;

		#endregion

		public SearchPageViewModel()
		{
			_searchCommand = new DelegateCommand(GetMovie, CanGetMovie);
			_togglePlotCommand = new DelegateCommand(TogglePlot);

			//SearchedMovie.PropertyChanged += SearchedMovie_PropertyChanged; //THROWS EXCEPTION!!!!

			SearchedMovie = new MovieReminder_Models.Movie()
			{
				ID = "12344",
				//Title = "Tee",
				Year=2005,
				PlotSummary="blablablablablabla",
				PosterURI = "http://cdn.collider.com/wp-content/uploads/2016/06/stranger-things-poster-netflix1.jpg",
				Theater = DateTime.Today.AddDays(10),
				Dvd = DateTime.Today.AddDays(20)
			};
		}

		bool CanGetMovie()
		{
			bool canGetMovie = false;

			if (SearchedMovie.Title !=null)
			{
				canGetMovie = SearchedMovie.Title.Length > 1;
			}
			else
			{
				canGetMovie = true;
			}

			return canGetMovie;
		}

	    void GetMovie()
		{
			try
			{
				mvService = new MovieReminder_MovieDBWrap.MovieService();

				FoundMoviesList = new List<MovieReminder_Models.Movie>();

				FoundMoviesList = mvService.SearchMovie(SearchedMovie.Title);

				if (FoundMoviesList.Count>0)
				{
					SearchedMovie = FoundMoviesList[0];
				}

				IsMovieFound = true;
			}
			catch(Exception ex)
			{
				
			}
		}

		void SearchedMovie_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			SearchCommand.RaiseCanExecuteChanged();
		}

		void TogglePlot()
		{
			IsPlotVisible = !IsPlotVisible;
		}
	}
}

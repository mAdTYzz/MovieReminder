using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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

		#endregion

		public SearchPageViewModel()
		{
			_searchCommand = new DelegateCommand(GetMovie, CanGetMovie);
			_togglePlotCommand = new DelegateCommand(TogglePlot);

			//SearchedMovie.PropertyChanged += SearchedMovie_PropertyChanged; //THROWS EXCEPTION!!!!

			SearchedMovie = new MovieReminder_Models.Movie();
		}

		private bool CanGetMovie()
		{
			bool canGetMovie = false;
			IsMovieFound = false;

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

	    private void GetMovie()
		{
			try
			{
				//Reseting the old values
				SearchedMovie = new MovieReminder_Models.Movie(){Title=SearchedMovie.Title};

				MovieService mvService = new MovieService(){MovieTitle=SearchedMovie.Title};

				SearchedMovie= mvService.SearchMovie();

				if (SearchedMovie.FoundWithApi==true)
				{
					IsMovieFound = true;
				}
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

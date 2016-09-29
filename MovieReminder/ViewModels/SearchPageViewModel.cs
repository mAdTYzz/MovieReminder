using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;

namespace MovieReminder //TEST WITH MOVIE: FRIEND REQUEST (THEATER: 2016-10-07, DVD: N/A)
{
	public class SearchPageViewModel : BindableBase
	{
		#region Commands, Properties and other good

		#region Commands
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

		private DelegateCommand _clearContentCommand;
		public DelegateCommand ClearContentCommand
		{
			get { return _clearContentCommand; }
			set { SetProperty(ref _clearContentCommand, value); }
		}

		private DelegateCommand _toggleMainInfoCommand;
		public DelegateCommand ToggleMainInfoCommand
		{
			get { return _toggleMainInfoCommand; }
			set { SetProperty(ref _toggleMainInfoCommand, value); }
		}

		private DelegateCommand _toggleCastCommand;
		public DelegateCommand ToggleCastCommand
		{
			get { return _toggleCastCommand; }
			set { SetProperty(ref _toggleCastCommand, value); }
		}

		private DelegateCommand _addTheaterReminderCommand;
		public DelegateCommand AddTheaterReminderCommand
		{
			get { return _addTheaterReminderCommand; }
			set { SetProperty(ref _addTheaterReminderCommand, value); }
		}
		#endregion

		#region Properties
		private MovieReminder_Models.Movie _searchedMovie;
		public MovieReminder_Models.Movie SearchedMovie
		{
			get { return _searchedMovie; }
			set { SetProperty(ref _searchedMovie, value); }
		}

		private string _searchPageInformationLabel = "Search Movie";
		public string SearchPageInformationLabel
		{
			get { return _searchPageInformationLabel; }
			set
			{
				if (SearchedMovie.imdbID != null)
				{
					_searchPageInformationLabel = "Movie Found";
				}
				else
					_searchPageInformationLabel = "Movie not found!";

				SetProperty(ref _searchPageInformationLabel, value);
			}
		}

		//private List<MovieReminder_Models.Movie> _foundMoviesList;
		//public List<MovieReminder_Models.Movie> FoundMoviesList
		//{
		//	get { return _foundMoviesList; }
		//	set { SetProperty(ref _foundMoviesList, value); }
		//}
		#endregion

		#region Var

		private bool _isPosterAvailable;
		public bool IsPosterAvailable
		{
			get { return _isPosterAvailable; }
			set
			{
				if (String.IsNullOrWhiteSpace(SearchedMovie.Poster) == false)
				{
					value = true;
				}
				else
					value = false;

				SetProperty(ref _isPosterAvailable, value);
			}
		}

		private bool _isPlotVisible=false;
		public bool IsPlotVisible
		{
			get { return _isPlotVisible; }
			set { SetProperty(ref _isPlotVisible, value); }
		}

		private bool _isSearchFieldVisible=true;
		public bool IsSearchFieldVisible
		{
			get { return _isSearchFieldVisible; }
			set { SetProperty(ref _isSearchFieldVisible, value); }
		}

		private bool _isInfoFieldVisible=false;
		public bool IsInfoFieldVisible
		{
			get { return _isInfoFieldVisible; }
			set { SetProperty(ref _isInfoFieldVisible, value); }
		}

		private bool _isCastVisible=false;
		public bool IsCastVisible
		{
			get { return _isCastVisible; }
			set { SetProperty(ref _isCastVisible, value); }
		}

		private IReminderService _reminderService;

		#endregion

		#endregion

		public SearchPageViewModel(IReminderService reminderService)
		{
			_searchCommand = new DelegateCommand(GetMovie, CanGetMovie);
			_togglePlotCommand = new DelegateCommand(TogglePlot);
			_clearContentCommand = new DelegateCommand(ClearContent);
			_toggleMainInfoCommand = new DelegateCommand(ToggleMainInfo);
			_toggleCastCommand = new DelegateCommand(ToggleCast);
			_addTheaterReminderCommand = new DelegateCommand(AddTheaterReminder);//, CanAddTheaterReminder);

			_reminderService = reminderService;

			SearchedMovie = new MovieReminder_Models.Movie();
			SearchedMovie.PropertyChanged += SearchedMovie_PropertyChanged;

			//REMOVE LATER!!!
			//ChangeDateAndTitleForTest(); 
		}


		//CHANGING THATER RELEASE DATE TO TOMORROW FOR TESTING OF THE REMINDER
		void ChangeDateAndTitleForTest()
		{
			SearchedMovie.TheaterReleaseDate = DateTime.Today.AddDays(1);

			SearchedMovie.Title = "TestMovieTitle";
			SearchedMovie.Plot = "BLABLABLABLA AND BLABLABLA";
		}

		void SearchedMovie_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//SearchCommand.RaiseCanExecuteChanged();
			TogglePlotCommand.RaiseCanExecuteChanged();
			ClearContentCommand.RaiseCanExecuteChanged();
			ToggleMainInfoCommand.RaiseCanExecuteChanged();
			AddTheaterReminderCommand.RaiseCanExecuteChanged();
		}

		private bool CanGetMovie()
		{
			bool canGetMovie = false;
			IsPosterAvailable = false;

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

				SearchedMovie = mvService.SearchMovie();

				if (String.IsNullOrWhiteSpace(SearchedMovie.imdbID)==false)
				{
					IsSearchFieldVisible = false;
					if (String.IsNullOrWhiteSpace(SearchedMovie.Poster) == false)
					{
						IsPosterAvailable = true;
					}
				}
			}
			catch(Exception ex)
			{
				
			}
		}

		private void TogglePlot()
		{
			if (SearchedMovie!=null && SearchedMovie.Plot!=null && SearchedMovie.Plot.Length>1)
			{
				IsPlotVisible = !IsPlotVisible;
			}
		}

		private void ClearContent()
		{
			SearchedMovie = new MovieReminder_Models.Movie();
			IsSearchFieldVisible = true;
			IsPosterAvailable = false;
			IsInfoFieldVisible = false;
			IsCastVisible = false;
		}

		void ToggleMainInfo()
		{
			if (SearchedMovie != null && SearchedMovie.Year > 0)
			{
				IsInfoFieldVisible = !IsInfoFieldVisible;
			}
		}

		void ToggleCast()
		{
			if (SearchedMovie != null && SearchedMovie.Cast != null && SearchedMovie.Cast.Count > 0)
			{
				IsCastVisible = !IsCastVisible;
			}
		}

		//bool CanAddTheaterReminder()
		//{
		//	bool canDo = false;

		//	if (SearchedMovie.TheaterReleaseDate > DateTime.Today && String.IsNullOrWhiteSpace(SearchedMovie.Title)==false)
		//	{
		//		canDo = true;
		//	}

		//	return canDo;
		//}

		void AddTheaterReminder()
		{
			if (SearchedMovie.TheaterReleaseDate > DateTime.Today && String.IsNullOrWhiteSpace(SearchedMovie.Title) == false)
			{
				DateTime reminderDate = SearchedMovie.TheaterReleaseDate.AddDays(-1); //Setting the date back with one day...
				if (_reminderService.AddReminder(reminderDate, EventTitleSetter(), EventNotesSetter()) == true)
				{
					ClearContent();
				}
				else
					SearchedMovie.Title = "Error!";
			}
		}

		string EventTitleSetter()
		{
			return SearchedMovie.Title + " is showing tomorrow! "; 
		}

		string EventNotesSetter() //#MOVIETITLE is used to check if the event is allready set
		{
			string notes = string.Empty;

			if (String.IsNullOrWhiteSpace(SearchedMovie.Plot) == false)
			{
				notes = SearchedMovie.Plot + String.Format("\n#{0}",SearchedMovie.Title);
			}
			else
				notes = String.Format("#{0}", SearchedMovie.Title);

			return notes;
		}
	}
}

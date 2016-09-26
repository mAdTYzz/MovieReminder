using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Xamarin.Forms;

namespace MovieReminder_Models
{
	public class Movie:BindableBase
	{
		
		private string _imdbID;
		public string imdbID
		{
			get { return _imdbID; }
			set { SetProperty(ref _imdbID, value); }
		}
		
		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		
		private int _year;
		public int Year
		{
			get { return _year; }
			set { SetProperty(ref _year, value); }
		}

		private string _plot;
		public string Plot
		{
			get { return _plot; }
			set { SetProperty(ref _plot, value); }
		}

		private string _released;
		public string Released
		{
			get { return _released; }
			set { SetProperty(ref _released, value); }
		}

		private DateTime _theaterReleaseDate;
		public DateTime TheaterReleaseDate
		{
			get { return _theaterReleaseDate; }
			set { SetProperty(ref _theaterReleaseDate, value); }
		}

		private string _dvd;
		public string Dvd
		{
			get { return _dvd; }
			set { SetProperty(ref _dvd, value); }
		}

		private DateTime _dvdReleaseDate;
		public DateTime DvdReleaseDate
		{
			get { return _dvdReleaseDate; }
			set { SetProperty(ref _dvdReleaseDate, value); }
		}

		private string _poster;
		public string Poster
		{
			get { return _poster; }
			set { SetProperty(ref _poster, value); }
		}

		private string _genre; 
		public string Genre
		{
			get { return _genre; }
			set { SetProperty(ref _genre, value); }
		}

		private string _runtime;
		public string Runtime
		{
			get { return _runtime; }
			set { SetProperty(ref _runtime, value); }
		}

		private string _actors;//Here come the actors in the same string. API style. They get split and saved in the Cast list
		public string Actors
		{
			get { return _actors; }
			set 
			{ 
				SetProperty(ref _actors, value); 

				if (String.IsNullOrWhiteSpace(value)==false)
				{
					InitializeCastCollection();
				}
			}
		}

		private ObservableCollection<Actor> _cast;
		public ObservableCollection<Actor> Cast
		{
			get { return _cast; }
			set 
			{ 
				try
				{
					if (String.IsNullOrWhiteSpace(Actors) == false)
					{
						value = new ObservableCollection<Actor>();
						string substring = string.Empty;
						Actor actor = new Actor();

						foreach (var character in Actors)
						{
							if (character == ',')
							{
								actor.Name = substring;
								value.Add(actor);
								substring = string.Empty;
								actor = new Actor();
							}
							if (Actors.IndexOf(character) == Actors.Length - 1 && character!=',') //In case of last name
							{
								substring += character; //The last character needs to be added
								actor.Name = substring;
								value.Add(actor);
								substring = string.Empty;
								actor = new Actor();
							}
							else if (character!=',')
							{
								substring += character;
							}
						}
					}
					SetProperty(ref _cast, value);
				}
				catch (Exception ex)
				{
					SetProperty(ref _cast, null);
				}
			}
		}

		private decimal _imdbRating;
		public decimal imdbRating
		{
			get { return _imdbRating; }
			set { SetProperty(ref _imdbRating, value); }
		}

		private bool _foundWithApi=false;
		public bool FoundWithApi
		{
			get { return _foundWithApi; }
			set { SetProperty(ref _foundWithApi, value); }
		}

		public Movie()
		{
				
		}

		private void InitializeCastCollection()
		{
			Cast = new ObservableCollection<Actor>();
		}
	}
}

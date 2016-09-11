using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace MovieReminder_Models
{
	public class Movie:BindableBase
	{
		
		private string _id;
		public string ID
		{
			get { return _id; }
			set { SetProperty(ref _id, value); }
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

		private bool _foundWithApi=false;
		public bool FoundWithApi
		{
			get { return _foundWithApi; }
			set { SetProperty(ref _foundWithApi, value); }
		}

		public Movie()
		{
		}
	}
}

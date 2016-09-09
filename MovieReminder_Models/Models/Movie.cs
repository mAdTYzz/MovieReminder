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

		private string _plotSummary;
		public string PlotSummary
		{
			get { return _plotSummary; }
			set { SetProperty(ref _plotSummary, value); }
		}

		private DateTime _theater;
		public DateTime Theater
		{
			get { return _theater; }
			set { SetProperty(ref _theater, value); }
		}

		private DateTime _dvd;
		public DateTime Dvd
		{
			get { return _dvd; }
			set { SetProperty(ref _dvd, value); }
		}

		private string _posterURI;
		public string PosterURI
		{
			get { return _posterURI; }
			set { SetProperty(ref _posterURI, value); }
		}

		public Movie()
		{
		}
	}
}

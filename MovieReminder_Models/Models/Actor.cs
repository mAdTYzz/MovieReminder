using System;
using Prism.Mvvm;

namespace MovieReminder_Models
{
	public class Actor:BindableBase
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}

		public Actor()
		{
		}
	}
}

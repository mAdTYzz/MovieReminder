using System;
using System.Collections.Generic;
using Prism.Unity;
using Prism.Mvvm;
using Xamarin.Forms;

namespace MovieReminder
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer)
		{

		}

		protected override void OnInitialized()
		{
			InitializeComponent();

			//NavigationService.NavigateAsync("MainPage");
			NavigationService.NavigateAsync("MainPage", animated: false);
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<MainPage>();
			Container.RegisterTypeForNavigation<SearchPage>();
			Container.RegisterTypeForNavigation<UpcomingPage>();
		}
	}
}

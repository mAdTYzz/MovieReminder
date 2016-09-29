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

			NavigationService.NavigateAsync("MainPage", animated: true);

			DependencyService.Get<IReminderService>(DependencyFetchTarget.GlobalInstance).StartService();

			//ResourceDictionary res = new ResourceDictionary();
			//res.Add(LayoutContainer.ButtonStyle);
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<MainPage>();
			Container.RegisterTypeForNavigation<SearchPage>();
			Container.RegisterTypeForNavigation<UpcomingPage>();
		}
	}

	//public static class LayoutContainer
	//{
	//	public static Style ButtonStyle
	//	{
	//		get;
	//		set;
	//	}

	//	static LayoutContainer()
	//	{
	//		try
	//		{
	//			ButtonStyle = new Style(typeof(Button))
	//			{
	//				Setters =
	//				{
	//					new Setter {Property=Button.FontSizeProperty, Value="16"},
	//					new Setter {Property=Button.FontAttributesProperty, Value="Bold"},
	//					new Setter {Property=Button.BorderWidthProperty, Value="5"}
	//				}
	//			};
	//		}
	//		catch (Exception ex)
	//		{

	//		}
	//	}
	//}
}

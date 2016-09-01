using Prism.Unity;
using Xamarin.Forms;

namespace MovieReminder
{
	public partial class App : PrismApplication
	{
		// add these methods
		protected override void OnInitialized()
		{
			NavigationService.NavigateAsync("MovieReminderPage");
		}

		protected override void RegisterTypes()
		{
			//Container.RegisterTypeForNavigation<MovieReminderPage,MovieReminderPageViewModel>();
			Container.RegisterTypeForNavigation<MovieReminderPage>();

		}
	}
}

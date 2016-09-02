using Xamarin.Forms;

namespace MovieReminder
{
	public partial class MovieReminderPage : TabbedPage
	{
		public MovieReminderPage()
		{
			InitializeComponent();

			Children.Add(new SearchPage());
			Children.Add(new UpcomingMoviesPage());
		}
	}
}

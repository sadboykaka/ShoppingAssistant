using ShoppingAssistant.Controllers;
using ShoppingAssistant.Logging;
using ShoppingAssistant.Models;
using ShoppingAssistant.Views;
using Xamarin.Forms;

namespace ShoppingAssistant
{
	/// <inheritdoc />
	/// <summary>
	/// Main class
	/// </summary>
	public partial class App
	{
		/// <summary>
		/// Logger reference
		/// </summary>
		public static ILog Log;

		/// <summary>
		/// ModelManager reference
		/// </summary>
		public static ModelManager ModelManager;
		
		/// <summary>
		/// GeolocationController reference
		/// </summary>
		public static GeolocationController GeolocationController;
		
		/// <summary>
		/// MasterDetailPage reference
		/// </summary>
		public static MDP Md { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public App ()
		{
			InitializeComponent();

			// Create logger
			Log = DependencyService.Get<ILog>();

			// Geolocation controller
			// Not user specific and takes some time so created early
			GeolocationController = new GeolocationController();
			
			// Create master controller
			ModelManager = new ModelManager();
			
			// Open Login View
			MainPage = new NavigationPage(new LoginView());
		}

		/// <summary>
		/// Method to log the user out
		/// Required at the application level as the root page needs to be changed
		/// </summary>
		public static void Logout()
		{
			ModelManager.Logout();
			Current.MainPage = new NavigationPage(new LoginView());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

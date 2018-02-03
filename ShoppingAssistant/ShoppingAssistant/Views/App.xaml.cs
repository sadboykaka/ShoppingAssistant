using ShoppingAssistant.Controllers;
using ShoppingAssistant.DependencyInterfaces;
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
        /// Boolean indicating if the application is running in the foreground or background
        /// </summary>
	    public static bool Minimised = false;

		/// <summary>
		/// Logger reference
		/// </summary>
		public static ILog Log;

		/// <summary>
		/// MasterController reference
		/// </summary>
		public static MasterController MasterController;
		
		/// <summary>
		/// GeolocationController reference
		/// </summary>
		public static GeolocationController GeolocationController;
		
		/// <summary>
		/// MasterDetailPage reference
		/// </summary>
		public static MDP Md { get; set; }

        /// <summary>
        /// Notification helper object
        /// </summary>
	    public static INotificationHelper NotificationHelper;

		/// <summary>
		/// Constructor
		/// </summary>
		public App ()
		{
			InitializeComponent();

			// Create logger
			Log = DependencyService.Get<ILog>();

            // Create notification helper
		    NotificationHelper = DependencyService.Get<INotificationHelper>();

			// Geolocation controller
			// Not user specific and takes some time so created early
			GeolocationController = new GeolocationController();
			
			// Create master controller
			MasterController = new MasterController();
			
			// Open Login View
			MainPage = new NavigationPage(new LoginView());
		}

		/// <summary>
		/// Method to log the user out
		/// Required at the application level as the root page needs to be changed
		/// </summary>
		public static void Logout()
		{
			MasterController.Logout();
			Current.MainPage = new NavigationPage(new LoginView());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		    Minimised = false;
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		    Minimised = true;
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		    Minimised = false;
		}
	}
}

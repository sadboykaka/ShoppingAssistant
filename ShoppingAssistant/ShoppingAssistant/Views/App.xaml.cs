using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Logging;
using ShoppingAssistant.Models;
using ShoppingAssistant.Views;
using Xamarin.Forms;

namespace ShoppingAssistant
{
	public partial class App : Application
	{
	    public static ModelManager ModelManager;
	    public static ILog Log;
        public static MDP MD { get; set; }

        public App ()
		{
			InitializeComponent();
		    Log = DependencyService.Get<ILog>();
            ModelManager = new ModelManager();


		    MD = new MDP();

		    Application.Current.MainPage = MD;

            //MainPage = new NavigationPage(new ShoppingAssistant.MainPage());
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

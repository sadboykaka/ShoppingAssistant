using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
    /// <summary>
    /// Master Detail Page class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDP : MasterDetailPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MDP()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        /// <summary>
        /// ListView item selected event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Return if data is bad
            if (!(e.SelectedItem is MDPMenuItem item))
                return;

            // Logout and drop out of method if Logout selected
            if (item.Title == "Logout")
            {
                App.Logout();
                return;
            }

            // Create the Detail page
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;   
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDPMaster : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public string IconSource { get; set; }


        public class MasterPageItem
        {
            public string Title { get; set; }
            public string IconSource { get; set; }
            public Type TargetType { get; set; }
        }
        

        public MDPMaster()
        {
            InitializeComponent();
            IconSource = "Icon.png";
            
            var masterPageItems = new List<MDPMenuItem>();
            
            masterPageItems.Add(new MDPMenuItem
            {
                Title = "Shopping Lists",
                IconSource = "shopping_list.png",
                TargetType = typeof(ShoppingListsView)
            });

            masterPageItems.Add(new MDPMenuItem
            {
                Title="Nearby Locations",
                IconSource = "cart.png",
                TargetType = typeof(NearbyLocationsView)
            });

            masterPageItems.Add(new MDPMenuItem
            {
                Title = "Logout",
                IconSource = "Icon.png",
                TargetType = typeof(LoginView)
            });

            listView.ItemsSource = masterPageItems;
        }
        
    }
}
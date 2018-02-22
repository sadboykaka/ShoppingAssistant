using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
    /// <summary>
    /// Master Detail Page providing the left hand context menu
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDPMaster : ContentPage
    {
        /// <summary>
        /// List of items
        /// Binding property
        /// </summary>
        public ListView ListView => listView;

        /// <summary>
        /// Menu Icon
        /// Binding property
        /// </summary>
        public string IconSource { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MDPMaster()
        {
            InitializeComponent();
            IconSource = "Icon.png";

            var masterPageItems = new List<MDPMenuItem>
            {
                new MDPMenuItem
                {
                    Title = "Shopping Lists",
                    IconSource = "shopping_list.png",
                    TargetType = typeof(ShoppingListsView)
                },
                new MDPMenuItem
                {
                    Title = "Nearby Locations",
                    IconSource = "cart.png",
                    TargetType = typeof(NearbyLocationsView)
                },
                new MDPMenuItem
                {
                    Title = "Logout",
                    IconSource = "Icon.png",
                    TargetType = typeof(LoginView)
                }
            };

            listView.ItemsSource = masterPageItems;
        }
        
    }
}
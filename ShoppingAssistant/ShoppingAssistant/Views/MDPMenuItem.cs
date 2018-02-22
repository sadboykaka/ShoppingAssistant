using System;
namespace ShoppingAssistant.Views
{
    /// <summary>
    /// Master detail page menu item
    /// </summary>
    public class MDPMenuItem
    {
        /// <summary>
        /// Icon source
        /// </summary>
        public string IconSource { get; set; }

        /// <summary>
        /// Title of the menu item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Type to create to spawn the required page
        /// </summary>
        public Type TargetType { get; set; }
    }
}
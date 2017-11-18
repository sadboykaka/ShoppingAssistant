using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ObjCRuntime;
using ShoppingAssistant.Controls;
using ShoppingAssistant.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace ShoppingAssistant.iOS.Controls
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeTextField = (UITextField) Control;
                nativeTextField.EditingDidBegin += (object sender, EventArgs eIos) => {
                    nativeTextField.PerformSelector(new Selector("selectAll"), null, 0.0f);
                };
            }
        }
    }
}
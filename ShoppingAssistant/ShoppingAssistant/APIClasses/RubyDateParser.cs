using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShoppingAssistant.APIClasses
{
    public static class RubyDateParser
    {
        public static DateTime ParseDateTime(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                if (date == null)
                {
                    App.Log.Error("ParseDateTime", "Date null");
                }

                App.Log.Error("ParseDateTime", "Date = " + date + "\n" + ex.Message);
            }

            // Return minvalue to allow future updates
            return DateTime.MinValue;
        }

        public static string ParseString(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        }

        public static int Compare(string first, string second)
        {
            return DateTime.Compare(ParseDateTime(first), ParseDateTime(second));
        }
    }
}

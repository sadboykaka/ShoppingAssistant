using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShoppingAssistant.APIClasses
{
    /// <summary>
    /// Static parser class that converts between Ruby date strings and DateTime objects
    /// </summary>
    public static class RubyDateParser
    {
        /// <summary>
        /// Static method to parse a DateTime from a Ruby formatted date string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static DateTime ParseDateTime(string date)
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

        /// <summary>
        /// Static method that returns a Ruby date string from a DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ParseString(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Static method that compares two Ruby date strings
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>
        /// Less than zero for first earlier than second
        /// Zero for first == second
        /// Greater than zero for first after second
        /// </returns>
        public static int Compare(string first, string second)
        {
            return DateTime.Compare(ParseDateTime(first), ParseDateTime(second));
        }
    }
}

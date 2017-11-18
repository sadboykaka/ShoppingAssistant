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
            return DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
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

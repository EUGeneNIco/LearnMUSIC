using System;
using System.Globalization;

namespace LearnMUSIC.Common.Helper
{
    public static class StringExtensions
    {
        public static bool ConvertToBoolean(this string str)
        {
            if (str == null)
            {
                return false;
            }

            var val = str.ToString();
            if (!bool.TryParse(val, out bool result))
            {
                return false;
            }

            return result;
        }

        public static int ConvertToInteger(this string value)
        {
            if (value == null)
            {
                return 0;
            }

            var val = value.ToString();
            int.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out int result);
            return result;
        }

        public static long ConvertToLong(this string value)
        {
            if (value == null)
            {
                return 0;
            }

            var val = value.ToString();
            long.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out long result);
            return result;
        }

        public static int? ConvertToNullableInteger(this string value)
        {
            if (value == null || value == "")
            {
                return null;
            }

            var val = value.ToString();
            int.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out int result);
            return result;
        }

        public static decimal ConvertToDecimal(this string value)
        {
            var val = value.ToString();
            decimal.TryParse(val, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result);
            return result;
        }

        public static double ConvertToDouble(this string value)
        {
            var val = value.ToString();
            double.TryParse(val, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double result);
            return result;
        }

        public static DateTime ConvertToDate(this string value)
        {
            var val = value.ToString();
            DateTime result;
            if (!DateTime.TryParse(val, out result))
            {
                return DateTime.Today;
            }

            return result;
        }
    }
}

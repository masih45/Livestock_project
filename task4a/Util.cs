using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4a
{
    class Util
    {
        public static readonly int ROUND = 2;
        public static readonly string BAD_STRING = string.Empty;
        public static readonly int BAD_INT = Int32.MinValue;
        public static readonly double BAD_DOUBLE = Double.MinValue;

        public static int ParseInt(object? o)
        {
            if (o == null) return BAD_INT;
            int n;
            if (int.TryParse(o.ToString(), out n) == false)
                return BAD_INT;
            return n;
        }

        public static double ParseDouble(object? o)
        {
            if (o == null) return BAD_DOUBLE;
            double n;
            if (double.TryParse(o.ToString(), out n) == false)
                return BAD_DOUBLE;
            return n;
        }

        public static string ToTitleCase(string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
        }

        public static string? VerifyType(string? type)
        {
            if (string.IsNullOrWhiteSpace(type)) return null;
            var types = new List<string>() { "Cow", "Sheep" };
            string t = ToTitleCase(type);
            return types.Contains(t) ? t : null;
        }
    }
}

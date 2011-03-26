using System;
using System.Collections.Generic;
using System.Text;

namespace libTravian
{
    public static class ExtendMethod
    {
        public static string Join(this Array array, string seperator)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (object o in array)
                sb.Append(o).Append(seperator);

            sb.Remove(sb.Length - seperator.Length, seperator.Length);
            return sb.ToString();
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace libTravian
{
    class HtmlUtility
    {
        public static string[] GetElementsWithClass(string data, string elementName, string elementClass)
        {
            string pattern = String.Format(
                @"<{0} class=""{1}"".+?</{0}>",
                elementName,
                elementClass);
            MatchCollection matches = Regex.Matches(
                data,
                pattern,
                RegexOptions.Singleline);

            string []tables = new string[matches.Count];
            for (int i = 0; i < matches.Count; i ++)
            {
                tables[i] = matches[i].Groups[0].Value;
            }

            return tables;
        }

        public static string GetElementWithClass(string data, string elementName, string elementClass)
        {
            string[] elements = HtmlUtility.GetElementsWithClass(data, elementName, elementClass);
            if (elements.Length > 0)
            {
                return elements[0];
            }

            return null;
        }

        public static string[] GetElements(string data, string elementName)
        {
            string pattern = String.Format(
                @"<{0}.+?</{0}>",
                elementName);
            MatchCollection matches = Regex.Matches(
                data,
                pattern,
                RegexOptions.Singleline);

            string[] tables = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                tables[i] = matches[i].Groups[0].Value;
            }

            return tables;
        }

        public static string GetElement(string data, string elementName)
        {
            string[] elements = HtmlUtility.GetElements(data, elementName);
            if (elements.Length > 0)
            {
                return elements[0];
            }

            return null;
        }
    }
}

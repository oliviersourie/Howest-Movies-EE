using System;
using System.Linq;

namespace Howest_Movies_EE_DAL.Extensions
{
    public static class StringExtensions
    {

        public static string TakeFirst(this string s, int length)
        {
            return $"{(s.Length > length ? s.Substring(0, length).Trim() : s)}{(s.Length >= length ? "..." : "")}";

        }

        public static string RemoveChars(this string s, string chars)
        {
            char[] characters = chars.ToCharArray();
            return s.Trim(characters);

        }

        public static string RemoveDoubleQuotes(this string s)
        {

            return s.Replace('"', ' ').Trim() ;

        }

        public static string GetCountryFromOriginalAirDate(this string s)
        {
            return s.Split(' ')
                    .ToList()
                    .Last()
                    .RemoveChars("()");
        }





    }
}

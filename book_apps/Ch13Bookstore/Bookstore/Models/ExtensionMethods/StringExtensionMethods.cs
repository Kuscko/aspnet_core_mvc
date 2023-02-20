using System.Text;

namespace Bookstore.Models
{
    // a series of extension methods that make it easier to create slugs, compare strings, capitalize strings, 
    // and cast a string to an int. Note that the EqualsNoCase() method doesn't work in EF Core code such as
    // 'Where(b => b.GenreId.EqualsNoCase("novel"))' In that case, must use old fashioned equality operator.

    public static class StringExtensions
    {
        public static string Slug(this string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c) || c == '-') { 
                    sb.Append(c);
                }
            }
            return sb.ToString().Replace(' ', '-').ToLower();
        }

        public static bool EqualsNoCase(this string s, string tocompare) =>
            s?.ToLower() == tocompare?.ToLower();

        public static int ToInt(this string s)
        {
            int.TryParse(s, out int id);
            return id;
        }

        public static string Capitalize(this string s) =>
            s?.Substring(0, 1)?.ToUpper() + s?.Substring(1);
    }
}
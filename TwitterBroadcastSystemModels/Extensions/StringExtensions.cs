using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class StringExtensions
    {
        public static string DecodeFromBase64(this string input)
        {
            if (string.IsNullOrEmpty(input) == true)
            {
                return string.Empty;
            }

            byte[] data = Convert.FromBase64String(input);
            string decodedString = Encoding.UTF8.GetString(data);

            return decodedString;
        }

        public static string Limit(this string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.Length >= length ? value.Substring(0, length) : value;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ContainsNumber(this string filter)
        {
            bool hasNumber = false;

            hasNumber = Regex.IsMatch(filter, "[0-9]");

            return hasNumber;
        }

        public static byte[] ToBytes(this string rtf)
        {
            return System.Text.Encoding.UTF8.GetBytes(rtf);
        }

        public static string ScrubPhone(this string inPhone)
        {
            return inPhone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace(" ", "");
        }

        public static string SanitizeSpecialCharacters(this string original)
        {
            return string.IsNullOrWhiteSpace(original) ? string.Empty :
                original.Replace(Environment.NewLine, "<br/>")
                        .Replace("\r", "<br/>")
                        .Replace("\n", "<br/>")
                        .Replace("'", "\\'")
                        .Replace("\\\\", "\\");
        }

        public static string UnSanitizeSpecialCharacters(this string original)
        {
            return string.IsNullOrWhiteSpace(original) ? string.Empty :
                original.Replace("<br/>", Environment.NewLine)
                        .Replace("\\'", "'");
        }

        public static string UseNonBreakingSpaces(this string original)
        {
            return (original ?? string.Empty).Replace(" ", "&nbsp;");
        }

        public static Guid? ToNullableGUID(this string str)
        {
            Guid resultGUID;
            if (Guid.TryParse(str, out resultGUID))
            {
                return resultGUID;
            }
            else
            {
                return (Guid?)null;
            }
        }

        public static String ReplaceIgnoreCase(this String str, String pattern, String replacement)
        {
            if(replacement == null)
            {
                replacement = String.Empty;
            }
            if(str == null || pattern == null)
            {
                return str;
            }
            return Regex.Replace(str, pattern, replacement, RegexOptions.IgnoreCase);
        }

        public static ulong? ToInt64(this String str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            try 
            {
                return Convert.ToUInt64(str);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace PocSocketMaui.Commons.Extensions
{
	public static class StringExtension
	{
		public static string DecodeFromBase64(this string base64String)
		{
			string base64Decoded;
			try
			{
				var data = Convert.FromBase64String(base64String);

				base64Decoded = Encoding.ASCII.GetString(data);
				return base64Decoded;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		public static bool IsNotNullOrWhiteSpace(this string data)
		{
			return !string.IsNullOrWhiteSpace(data);
		}

		public static bool IsNullOrWhiteSpace(this string data)
		{
			return string.IsNullOrWhiteSpace(data);
		}

		public static T FromJsonString<T>(this string data)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(data);
			}
			catch (Exception)
			{
				return default;
			}
		}

		public static string FirstCharToUpper(this string input)
		{
			switch (input)
			{
				case null:
					throw new ArgumentNullException(nameof(input));
				case "":
					throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
				default:
					return input[0].ToString().ToUpper() + input.Substring(1);
			}
		}

		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source?.IndexOf(toCheck, comp) >= 0;
		}

		public static string RemoveNotRequiredChars(this string oldStringValue, string valueToRemove, string valueToInsert)
		{
			return oldStringValue?.Replace(valueToRemove, valueToInsert);
		}

		public static bool ComplexContains(string source, string value)
		{
			var index = CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, value, CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace);
			return index != -1;
		}

		public static bool IsValidEmailAddress(this string s)
		{
			var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
			return regex.IsMatch(s);
		}

		public static bool IsValidUrl(this string text)
		{
			var rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
			return rx.IsMatch(text);
		}

		public static string TruncateWithSuffix(this string text, int maxLength, string suffix)
		{
			var truncatedString = text;

			if (maxLength <= 0)
				return truncatedString;

			var strLength = maxLength - suffix.Length;

			if (strLength <= 0)
				return truncatedString;

			if (text == null || text.Length <= maxLength)
				return truncatedString;

			truncatedString = text.Substring(0, strLength);
			truncatedString = truncatedString.TrimEnd();
			truncatedString += suffix;
			return truncatedString;
		}
	}
}


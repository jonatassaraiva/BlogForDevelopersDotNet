using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BlogForDevelopers.WebMvc3.App_Helpers
{
	public static class ContentHelper
	{
		public static string GetSrc(string input)
		{
			string pattern = "src=[\'|\"](.+?)[\'|\"]";

			Regex regexImageSrc = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

			Match match = regexImageSrc.Match(input);

			if (match.Success)
			{
				int lengeth = match.Value.Length;
				return match.Value.Substring(5, lengeth - 6);
			}
			else
				return string.Empty;
		}


	}
}
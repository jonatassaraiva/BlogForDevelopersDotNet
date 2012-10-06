using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogForDevelopers.WebMvc3.App_Code
{
	public static class ClassCssTagCloud
	{
		public static string GetClass(int max, int count)
		{
			int percent =  (int)(((double)count) / ((double)max) * 100);

			if (percent >= 80)
				return "xx-large";
			else if (percent >= 60)
				return "x-large";
			else if (percent >= 40)
				return "large";
			else if (percent >= 20)
				return "medium";
			else if (percent >= 10)
				return "small";
			else if (percent >= 5)
				return "x-small";
			else
				return "xx-small";
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR.Profile.Common
{
	public static class StringExtension
	{
		private const string HTTP_PREFIX = "http://";
		private const string HYPER_LINK_MASK = "<a href=\"{0}\">{1}</a>";

		public static int IndexOfByNumber(this string pString, char pChar, int pNumber)
		{
			if (String.IsNullOrWhiteSpace(pString))
				return -1;

			int result = -1;
			do
			{
				result++;
				if (pString[result] == pChar)
				{
					pNumber--;
				}
			}
			while (result < pString.Length && pNumber > 0);
			return result;
		}

		public static string ConvertHyperLinks(this string pString)
		{
			if (String.IsNullOrWhiteSpace(pString))
				return pString;

			int index = pString.IndexOf("http://", 0);
			if (index == -1)
				return pString;

			ICollection<string> links = new List<string>();
			while (index > 0)
			{
				index = pString.IndexOf("http://", index);
				if (index > 0)
				{
					int spaceIndex = pString.IndexOfAny(new char[] { ' ' , ',', }, index);
					if (spaceIndex > 0)
					{
						links.Add(pString.Substring(index, spaceIndex - index));
					}
					else
					{
						links.Add(pString.Substring(index, pString.Length - 1 - index));
					}
					index++;
				}
			}

			StringBuilder result = new StringBuilder();
			string[] str = pString.Split(links.ToArray<string>(), StringSplitOptions.None);
			for (int i = 0; i < links.Count; i++)
			{
				result.Append(str[i]);
				result.AppendFormat(HYPER_LINK_MASK, links.ElementAt(i), "[link]");
			}
			result.Append(str[str.Length - 1]);
			return result.ToString();
		}

	}

}

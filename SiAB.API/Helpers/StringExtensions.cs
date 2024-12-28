using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SiAB.API.Helpers
{
	public static class StringExtensions
	{
		public static bool CompareStrings(string str1, string str2)
		{
			if (str1 == null || str2 == null)
				return false;

			string normalizedStr1 = Regex.Replace(str1, @"\s+", "").ToLower();
			string normalizedStr2 = Regex.Replace(str2, @"\s+", "").ToLower();

			return normalizedStr1 == normalizedStr2;
		}

		public static Expression<Func<string, string, bool>> CompareStringsExpression()
		{
			return (str1, str2) => CompareStrings(str1, str2);
		}
	}
}

using System.Text.RegularExpressions;

namespace _Project.Scripts.Runtime.Utility
{
  public static class StringExtensions
  {
    public static string AddSpaceAfterCapital(this string input) =>
      Regex.Replace(input, "([A-Z])", " $1").Trim();
  }
}
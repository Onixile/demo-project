using System.Text.RegularExpressions;

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Extensions
{
  public static class StringExtensions
  {
    public static string AddSpaceAfterCapital(this string input) =>
      Regex.Replace(input, "([A-Z])", " $1").Trim();
    
    public static string Remove(this string input, string target)
    {
      if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(target))
        return input;

      return input.Replace(target, string.Empty).Trim();
    }
  }
}
using System.Collections.Generic;

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Extensions
{
  public static class CollectionExtensions
  {
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection) =>
      collection == null || collection.Count == 0;

    public static int LastIndex<T>(this ICollection<T> collection) =>
      collection.Count - 1;
  }
}
using System.Collections.Generic;

namespace MyMovieDb.Common.Extensions
{
    public static class CollectionsExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}
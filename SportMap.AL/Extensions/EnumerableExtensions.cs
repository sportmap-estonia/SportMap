namespace SportMap.AL.Extensions
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> FilterIfNotNull<T, TVal>(
            this IEnumerable<T> source,
            TVal? value,
            Func<T, TVal, bool> predicate)
        {
            if (value is null)
                return source;

            return source.Where(item => predicate(item, value));
        }

        public static IReadOnlyList<T> AsReadonlyList<T>(this T item)
        {
            return new List<T> { item }.AsReadOnly();
        }
    }
}

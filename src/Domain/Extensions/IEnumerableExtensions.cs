namespace Nexus.Domain.Extensions;

internal static partial class Extensions
{
    public static bool ContainsAny<T>(this IEnumerable<T> current, IEnumerable<T> other)
    {
        foreach (var value in other)
        {
            if (current.Contains(value))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainsOnly<T>(this IEnumerable<T> current, IEnumerable<T> other)
    {
        foreach (var value in current)
        {
            if (!other.Contains(value))
            {
                return false;
            }
        }

        return true;
    }
}
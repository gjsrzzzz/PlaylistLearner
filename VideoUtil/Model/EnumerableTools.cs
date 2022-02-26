using System.Collections;
using System.Text;

namespace VideoUtil.Model;

public static class EnumerableTools
{
    public static string ToCommaDelimited<T>(this IEnumerable<T> items)
    {
        return ToDelimitedString(items, ", ");
    }

    public static string ToCommaDelimitedShort<T>(this IEnumerable<T> items)
    {
        return ToDelimitedString(items, ",");
    }

    public static string ToDelimitedString<T>(this IEnumerable<T> items, string delimiter)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var item in items)
        {
            if (builder.Length > 0)
            {
                builder.Append(delimiter);
            }

            builder.Append(item is string stringItem ? stringItem : item?.ToString());
        }

        return builder.ToString();
    }

    public static string ToNewLineDelimited<T>(this IEnumerable<T> items)
    {
        return ToDelimitedString(items, "\n");
    }

    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }

    public static IEnumerable<object> Flatten(this IEnumerable source) =>
        Flatten(source, obj => !(obj is string));

    public static IEnumerable<object> Flatten(this IEnumerable source, Func<IEnumerable, bool> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return Flatten(source, obj =>
        {
            if (obj is IEnumerable inner)
            {
                var array = inner.Cast<object>().ToArray();
                if (predicate(array)) return array;
            }

            return null;
        });
    }

    public static IEnumerable<object> Flatten(this IEnumerable source, Func<object, IEnumerable?> selector)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return _();

        IEnumerable<object> _()
        {
            var e = source.GetEnumerator();
            var stack = new Stack<IEnumerator>();

            stack.Push(e);

            try
            {
                while (stack.Any())
                {
                    e = stack.Pop();

                    reloop:

                    while (e.MoveNext())
                    {
                        if (selector(e.Current) is IEnumerable inner)
                        {
                            stack.Push(e);
                            e = inner.GetEnumerator();
                            goto reloop;
                        }
                        else
                        {
                            yield return e.Current;
                        }
                    }

                    (e as IDisposable)?.Dispose();
                    e = null;
                }
            }
            finally
            {
                (e as IDisposable)?.Dispose();
                foreach (var se in stack)
                    (se as IDisposable)?.Dispose();
            }
        }
    }
}

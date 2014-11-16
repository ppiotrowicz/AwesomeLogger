using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeTestLogger
{
    public static class Extensions
    {
        public static void Apply<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var element in collection)
            {
                action(element);
            }
        }

        public static void Apply<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            int index = 0;
            foreach (var element in collection)
            {
                action(element, index);
                index++;
            }
        }

        public static string Indent(this string text, int width)
        {
            var lines = text.Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(Environment.NewLine, lines.Select(x => "".PadLeft(width) + x));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howest_Movies_EE_DAL.Extensions
{
    public static class EnumarableExtensions
    {
        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            Random r = new Random();
            return list.ElementAt(r.Next(0, list.Count()));
        }

        public static IEnumerable<U> ApplyFunc<U, T>(this IEnumerable<T> list, Func<T, U> func)
        {
            foreach(T item in list)
            {
                yield return func(item);
            }
        }

        public static void ApplyAction<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach(T item in list)
            {
                action(item);
            }
        }
    }
}

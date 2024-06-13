using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public static class Extensions
{
    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }

    public static bool IsLast<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? true : false;
    }

    public static T RandomEnumValue<T>(this T src) where T : struct
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }

    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public static IEnumerable<T> StringsToEnums<T>(this IEnumerable<string> strs) where T : struct, IConvertible
    {
        Type t = typeof(T);

        var ret = new List<T>();

        if (t.IsEnum)
        {
            T outStr;
            foreach (var str in strs)
            {
                if (Enum.TryParse(str, out outStr))
                {
                    ret.Add(outStr);
                }
            }
        }

        return ret;
    }

}

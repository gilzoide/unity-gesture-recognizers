using System;
using System.Collections.Generic;

namespace Gilzoide.GestureRecognizers
{
    public static class ListUtils
    {
        public static IDisposable GetList<T>(IEnumerable<T> elements, out List<T> list)
        {
#if UNITY_2021_1_OR_NEWER
            var disposable = UnityEngine.Pool.ListPool<T>.Get(out list);
            list.AddRange(elements);
            return disposable;
#else
            list = new List<T>(elements);
            return null;
#endif
        }
    }
}

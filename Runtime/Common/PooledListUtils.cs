using System.Collections.Generic;

namespace Gilzoide.GestureRecognizers.Common
{
    public static class PooledListUtils
    {
#if UNITY_2021_1_OR_NEWER
        public static UnityEngine.Pool.PooledObject<List<T>> GetList<T>(IEnumerable<T> elements, out List<T> list)
        {
            var disposable = UnityEngine.Pool.ListPool<T>.Get(out list);
            list.AddRange(elements);
            return disposable;
        }
#else
        public static System.IDisposable GetList<T>(IEnumerable<T> elements, out List<T> list)
        {
            list = new List<T>(elements);
            return null;
        }
#endif
    }
}

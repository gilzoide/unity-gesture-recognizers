using System;

namespace Gilzoide.GestureRecognizers
{
    public static class Extensions
    {
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }
    }
}

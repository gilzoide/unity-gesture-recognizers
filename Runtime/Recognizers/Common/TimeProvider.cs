using System;
using UnityEngine;

namespace Gilzoide.GestureRecognizers.Recognizers.Common
{
    public enum TimeProvider
    {
        Time,
        UnscaledTime,
    }

    public static class TimeProviderExtensions
    {
        public static float GetTime(this TimeProvider timeProvider)
        {
            switch (timeProvider)
            {
                case TimeProvider.Time:
                    return Time.time;
                
                case TimeProvider.UnscaledTime:
                    return Time.unscaledTime;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(timeProvider));
            }
        }

        public static object WaitForSeconds(this TimeProvider timeProvider, float seconds)
        {
            switch (timeProvider)
            {
                case TimeProvider.Time:
                    return new WaitForSeconds(seconds);
                
                case TimeProvider.UnscaledTime:
                    return new WaitForSecondsRealtime(seconds);
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(timeProvider));
            }
        }
    }
}

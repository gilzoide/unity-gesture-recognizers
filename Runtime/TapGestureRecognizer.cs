using System;
using Gilzoide.GestureRecognizers.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    [Serializable]
    public class TapGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTouches = 1;
        [Min(1)] public int NumberOfTaps = 1;
        [Min(0)] public float MultiTapDelayWindow = 0.5f;
        public TimeProvider TimeProvider = TimeProvider.UnscaledTime;

        [Space]
        public UnityEvent OnTapRecognized;

        protected int _tapsRecognized = 0;
        protected float _lastTapTime;

        public void Clear()
        {
            _tapsRecognized = 0;
            _lastTapTime = TimeProvider.GetTime();
        }

        public override void TouchStarted(int touchId, Vector2 position)
        {
            base.TouchStarted(touchId, position);
            if (_tapsRecognized > 0 && TimeProvider.GetTime() > _lastTapTime + MultiTapDelayWindow)
            {
                Clear();
            }

            if (_touchTracker.Count == NumberOfTouches)
            {
                _tapsRecognized++;
                _lastTapTime = TimeProvider.GetTime();
            }
        }

        public override void TouchEnded(int touchId)
        {
            base.TouchEnded(touchId);
            
            if (_tapsRecognized == NumberOfTaps && TimeProvider.GetTime() <= _lastTapTime + MultiTapDelayWindow)
            {
                OnTapRecognized.Invoke();
                Clear();
            }
        }
    }
}

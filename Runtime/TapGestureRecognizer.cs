using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class TapGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTouches = 1;
        [Min(1)] public int NumberOfTaps = 1;
        [Min(0)] public float MultiTapDelayWindow = 0.5f;

        protected int _tapsRecognized = 0;
        protected float _lastTapTime;

        protected static float CurrentTime => Time.unscaledTime;

        public void Clear()
        {
            _tapsRecognized = 0;
            _lastTapTime = CurrentTime;
        }

        protected override void TouchStarted(int touchId, Vector2 position)
        {
            base.TouchStarted(touchId, position);
            if (_tapsRecognized > 0 && CurrentTime > _lastTapTime + MultiTapDelayWindow)
            {
                Clear();
            }

            if (_touchTracker.Count == NumberOfTouches)
            {
                _tapsRecognized++;
                _lastTapTime = CurrentTime;
            }
        }

        protected override void TouchEnded(int touchId)
        {
            base.TouchEnded(touchId);

            if (_tapsRecognized == NumberOfTaps && CurrentTime <= _lastTapTime + MultiTapDelayWindow)
            {
                OnGestureRecognized.Invoke();
                Clear();
            }
        }
    }
}

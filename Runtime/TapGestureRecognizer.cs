using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class TapGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTapsRequired = 1;
        [Min(1)] public int NumberOfTouchesRequired = 1;
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

            if (_touchTracker.Count == NumberOfTouchesRequired)
            {
                _tapsRecognized++;
                _lastTapTime = CurrentTime;

                if (_tapsRecognized == NumberOfTapsRequired)
                {
                    OnGestureRecognized.Invoke();
                    Clear();
                }
            }
        }
    }
}

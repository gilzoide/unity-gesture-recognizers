using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class TapGestureRecognizer : MonoBehaviour
    {
        [Min(1)] public int NumberOfTapsRequired = 1;
        [Min(1)] public int NumberOfTouchesRequired = 1;
        [Min(0)] public float MultiTapDelayWindow = 0.5f;
        public UnityEvent OnGestureRecognized;

        protected readonly TouchTracker _touchTracker = new TouchTracker();
        protected int _tapsRecognized = 0;
        protected float _lastTapTime;

        public Vector2? Position => _touchTracker.Centroid;

        protected static float CurrentTime => Time.unscaledTime;

        public void Clear()
        {
            _tapsRecognized = 0;
            _lastTapTime = CurrentTime;
        }

        protected void TouchStarted(int touchId, Vector2 position)
        {
            _touchTracker.TouchStarted(touchId, position);
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

        protected void TouchEnded(int touchId)
        {
            _touchTracker.TouchEnded(touchId);
        }
    }
}

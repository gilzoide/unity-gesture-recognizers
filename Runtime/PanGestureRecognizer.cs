using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class PanGestureRecognizer : MonoBehaviour
    {
        [Min(1)] public int MinimumTouchesRequired = 1;
        [Min(1)] public int MaximumTouchesRequired = 1;
        public UnityEvent OnGestureRecognized;
        public UnityEvent<Vector2> OnPositionChanged;

        protected readonly TouchTracker _touchTracker = new TouchTracker();

        public Vector2? Position => _touchTracker.Centroid;
        public bool IsPanning => _touchTracker.Count.IsBetween(MinimumTouchesRequired, MaximumTouchesRequired);

        protected void TouchStarted(int touchId, Vector2 position)
        {
            _touchTracker.TouchStarted(touchId, position);
            if (IsPanning)
            {
                OnGestureRecognized.Invoke();
            }
        }

        protected void TouchMoved(int touchId, Vector2 position)
        {
            _touchTracker.TouchMoved(touchId, position);
            if (IsPanning)
            {
                OnPositionChanged.Invoke(Position.Value);
            }
        }

        protected void TouchEnded(int touchId)
        {
            _touchTracker.TouchEnded(touchId);
        }
    }
}

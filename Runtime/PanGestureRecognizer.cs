using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class PanGestureRecognizer : GestureRecognizer
    {
        [Min(1)] public int MinimumTouchesRequired = 1;
        [Min(1)] public int MaximumTouchesRequired = 1;
        public UnityEvent<Vector2> OnPositionChanged;

        public bool IsPanning => TouchCount.IsBetween(MinimumTouchesRequired, MaximumTouchesRequired);

        protected override void TouchStarted(int touchId, Vector2 position)
        {
            base.TouchStarted(touchId, position);
            if (IsPanning)
            {
                OnGestureRecognized.Invoke();
            }
        }

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);
            if (IsPanning)
            {
                OnPositionChanged.Invoke(Centroid.Value);
            }
        }
    }
}

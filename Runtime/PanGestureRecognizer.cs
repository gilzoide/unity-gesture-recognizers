using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class PanGestureRecognizer : ContinuousGestureRecognizer
    {
        [Min(1)] public int MinimumTouchesRequired = 1;
        [Min(1)] public int MaximumTouchesRequired = 1;
        public UnityEvent<Vector2> OnPositionChanged;

        public bool IsPanning => TouchCount.IsBetween(MinimumTouchesRequired, MaximumTouchesRequired);
        
        protected Vector2 _initialCentroid;

        protected override void TouchStarted(int touchId, Vector2 position)
        {
            bool wasPanning = IsPanning;
            base.TouchStarted(touchId, position);
            if (IsPanning && !wasPanning)
            {
                _initialCentroid = Centroid.Value;
                OnGestureRecognized.Invoke();
            }
        }

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);
            if (IsPanning)
            {
                OnPositionChanged.Invoke(Centroid.Value - _initialCentroid);
            }
        }

        protected override void TouchEnded(int touchId)
        {
            bool wasPanning = IsPanning;
            base.TouchEnded(touchId);
            if (!IsPanning && wasPanning)
            {
                OnGestureEnded.Invoke();
            }
        }
    }
}

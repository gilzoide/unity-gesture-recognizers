using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public abstract class PanGestureRecognizer : ContinuousGestureRecognizer
    {
        [Min(1)] public int NumberOfTouchesRequired = 1;
        public UnityEventVector2 OnPositionChanged;

        public bool IsPanning => TouchCount >= NumberOfTouchesRequired;
        
        protected Vector2 _initialCentroid;

        protected override void TouchStarted(int touchId, Vector2 position)
        {
            if (IsPanning)
            {
                return;
            }

            base.TouchStarted(touchId, position);
            if (IsPanning)
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

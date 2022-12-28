using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class PinchGestureRecognizer : AContinuousGestureRecognizer
    {
        [Min(2)] public int NumberOfTouchesRequired = 2;
        public UnityEventFloat OnScaleChanged;

        public bool IsPinching => TouchCount >= NumberOfTouchesRequired;
        public float Scale => IsPinching ? CurrentScale : 1;

        protected float _initialDistanceToCentroid = 1;
        protected float CurrentScale => AverageDistanceToCentroid.Value / _initialDistanceToCentroid;

        protected override void TouchStarted(int touchId, Vector2 position)
        {
            if (IsPinching)
            {
                return;
            }

            base.TouchStarted(touchId, position);
            if (IsPinching)
            {
                _initialDistanceToCentroid = Mathf.Max(0.0001f, AverageDistanceToCentroid.Value);
                OnGestureRecognized.Invoke();
            }
        }

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);
            if (IsPinching)
            {
                OnScaleChanged.Invoke(CurrentScale);
            }
        }

        protected override void TouchEnded(int touchId)
        {
            bool wasPinching = IsPinching;
            base.TouchEnded(touchId);
            if (!IsPinching && wasPinching)
            {
                OnGestureEnded.Invoke();
            }
        }
    }
}

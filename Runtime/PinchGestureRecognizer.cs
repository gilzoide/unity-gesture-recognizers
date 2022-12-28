using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class PinchGestureRecognizer : AContinuousGestureRecognizer
    {
        [Min(2)] public int NumberOfTouches = 2;
        [Header("Pinch events")]
        public UnityEventFloat OnScaleChanged;
        public UnityEventFloat OnScaleDelta;

        public bool IsPinching => TouchCount >= NumberOfTouches;
        public float Scale => IsPinching ? CurrentScale : 1;

        protected float CurrentScale => AverageDistanceToCentroid.Value / _initialDistanceToCentroid;
        protected float _initialDistanceToCentroid = 1;
        protected float _lastScale;
        protected bool _firstMove;

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
                _lastScale = 1;
                _firstMove = true;
            }
        }

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);
            if (IsPinching)
            {
                if (_firstMove)
                {
                    _firstMove = false;
                    OnGestureRecognized.Invoke();
                }
                
                float currentScale = CurrentScale;
                OnScaleChanged.Invoke(currentScale);
                OnScaleDelta.Invoke(currentScale - _lastScale);
                _lastScale = currentScale;
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

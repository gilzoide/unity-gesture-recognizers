using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class PinchGestureRecognizer : AContinuousGestureRecognizer
    {
        [Header("Options")]
        [Min(2)] public int NumberOfTouches = 2;
        
        [Header("Pinch events")]
        public UnityEventFloat OnScaleDelta;
        public UnityEventFloat OnScaleChanged;

        public bool IsPinching => TouchCount >= NumberOfTouches;
        public float Scale => IsPinching ? GetCurrentScale() : 1;

        protected float _initialDistanceToCentroid = 1;
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
                _firstMove = true;
            }
        }

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            if (!IsPinching)
            {
                base.TouchMoved(touchId, position);
                return;
            }

            if (_firstMove)
            {
                _firstMove = false;
                OnGestureRecognized.Invoke();
            }

            float previousScale = GetCurrentScale();

            base.TouchMoved(touchId, position);
   
            float currentScale = GetCurrentScale();
            OnScaleDelta.Invoke(currentScale - previousScale);
            OnScaleChanged.Invoke(currentScale);
        }

        protected override void TouchEnded(int touchId)
        {
            bool wasPinching = IsPinching;
            base.TouchEnded(touchId);
            if (!IsPinching && wasPinching)
            {
                _firstMove = false;
                OnGestureEnded.Invoke();
            }
        }

        protected float GetCurrentScale()
        {
            return AverageDistanceToCentroid.Value / _initialDistanceToCentroid;
        }
    }
}

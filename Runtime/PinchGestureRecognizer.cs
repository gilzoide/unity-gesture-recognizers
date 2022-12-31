using System;
using Gilzoide.GestureRecognizers.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    [Serializable]
    public class PinchGestureRecognizer : AGestureRecognizer
    {
        [Min(2)] public int NumberOfTouches = 2;
        
        [Space]
        public UnityEvent OnPinchStarted;
        public UnityEventFloat OnScaleDelta;
        public UnityEventFloat OnScaleChanged;
        public UnityEvent OnGestureEnded;

        public bool IsPinching => TouchCount >= NumberOfTouches;
        public float Scale => IsPinching ? GetCurrentScale() : 1;

        protected float _initialDistanceToCentroid = 1;
        protected bool _firstMove;

        public override void TouchStarted(int touchId, Vector2 position)
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

        public override void TouchMoved(int touchId, Vector2 position)
        {
            if (!IsPinching)
            {
                base.TouchMoved(touchId, position);
                return;
            }

            if (_firstMove)
            {
                _firstMove = false;
                OnPinchStarted.Invoke();
            }

            float previousScale = GetCurrentScale();

            base.TouchMoved(touchId, position);
   
            float currentScale = GetCurrentScale();
            OnScaleDelta.Invoke(currentScale - previousScale);
            OnScaleChanged.Invoke(currentScale);
        }

        public override void TouchEnded(int touchId)
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

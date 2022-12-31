using System;
using Gilzoide.GestureRecognizers.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    [Serializable]
    public class PanGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTouches = 1;
        
        [Space]
        public UnityEvent OnPanStarted;
        public UnityEventVector2 OnPositionDelta;
        public UnityEventVector2 OnPositionChanged;
        public UnityEvent OnGestureEnded;

        public bool IsPanning => TouchCount >= NumberOfTouches;
        public Vector2 Position => IsPanning ? GetPosition() : Vector2.zero;
        
        protected bool _firstMove;

        public override void TouchStarted(int touchId, Vector2 position)
        {
            if (IsPanning)
            {
                return;
            }

            base.TouchStarted(touchId, position);
            if (IsPanning)
            {
                _firstMove = true;
            }
        }

        public override void TouchMoved(int touchId, Vector2 position)
        {
            if (!IsPanning)
            {
                base.TouchMoved(touchId, position);
                return;
            }

            if (_firstMove)
            {
                _firstMove = false;
                OnPanStarted.Invoke();
            }

            Vector2 previousPosition = GetPosition();
            
            base.TouchMoved(touchId, position);

            Vector2 currentPosition = GetPosition();
            OnPositionDelta.Invoke(currentPosition - previousPosition);
            OnPositionChanged.Invoke(currentPosition);
        }

        public override void TouchEnded(int touchId)
        {
            bool wasPanning = IsPanning;
            base.TouchEnded(touchId);
            if (!IsPanning && wasPanning)
            {
                _firstMove = false;
                OnGestureEnded.Invoke();
            }
        }

        protected Vector2 GetPosition()
        {
            return Centroid.Value;
        }
    }
}

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
        public UnityEventPanGesture OnPanStarted;
        public UnityEventPanGesture OnPanRecognized;
        public UnityEvent OnGestureEnded;

        public bool IsPanning => TouchCount >= NumberOfTouches;
        public Vector2 Position => IsPanning ? GetPosition() : Vector2.zero;
        
        protected bool _firstMove;
        protected Vector2 _initialPosition;

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

            Vector2 previousPosition = GetPosition();

            if (_firstMove)
            {
                _firstMove = false;
                _initialPosition = previousPosition;
                OnPanStarted.Invoke(new PanGesture
                {
                    NumberOfTouches = NumberOfTouches,
                    InitialPosition = _initialPosition,
                    Position = previousPosition,
                    Delta = Vector2.zero,
                });
            }
            
            base.TouchMoved(touchId, position);

            Vector2 currentPosition = GetPosition();
            OnPanRecognized.Invoke(new PanGesture
            {
                NumberOfTouches = NumberOfTouches,
                InitialPosition = _initialPosition,
                Position = currentPosition,
                Delta = currentPosition - previousPosition,
            });
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

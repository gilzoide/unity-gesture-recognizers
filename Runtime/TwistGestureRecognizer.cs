using System;
using System.Collections.Generic;
using System.Linq;
using Gilzoide.GestureRecognizers.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    [Serializable]
    public class TwistGestureRecognizer : AGestureRecognizer
    {
        [Min(2)] public int NumberOfTouches = 2;
        
        [Space]
        public UnityEventTwistGesture OnTwistStarted = new UnityEventTwistGesture();
        public UnityEventTwistGesture OnTwistRecognized = new UnityEventTwistGesture();
        public UnityEvent OnGestureEnded = new UnityEvent();

        public bool IsTwisting => TouchCount >= NumberOfTouches;
        public float Rotation => IsTwisting ? _accumulatedRotation : 0;

        protected float _accumulatedRotation;
        protected bool _firstMove;

        public override void TouchStarted(int touchId, Vector2 position)
        {
            if (IsTwisting)
            {
                return;
            }

            base.TouchStarted(touchId, position);
            if (IsTwisting)
            {
                _accumulatedRotation = 0;
                _firstMove = true;
            }
        }

        public override void TouchMoved(int touchId, Vector2 position)
        {
            if (!IsTwisting)
            {
                base.TouchMoved(touchId, position);
                return;
            }

            Vector2 centroid = Centroid.Value;

            if (_firstMove)
            {
                _firstMove = false;
                OnTwistStarted.Invoke(new TwistGesture
                {
                    NumberOfTouches = NumberOfTouches,
                    Center = centroid,
                    Rotation = 0,
                    Delta = 0,
                });
            }

            using (PooledListUtils.GetList(_touchTracker.EnumerateTouchVectors(), out List<Vector2> previousVectors))
            {
                base.TouchMoved(touchId, position);

                IEnumerable<Vector2> currentVectors = _touchTracker.EnumerateTouchVectors();
                float deltaRotation = previousVectors.Zip(currentVectors, Vector2.SignedAngle).Average();
                _accumulatedRotation += deltaRotation;
                OnTwistRecognized.Invoke(new TwistGesture
                {
                    NumberOfTouches = NumberOfTouches,
                    Center = centroid,
                    Rotation = _accumulatedRotation,
                    Delta = deltaRotation,
                });
            }
        }

        public override void TouchEnded(int touchId)
        {
            bool wasPinching = IsTwisting;
            base.TouchEnded(touchId);
            if (!IsTwisting && wasPinching)
            {
                _firstMove = false;
                OnGestureEnded.Invoke();
            }
        }
    }
}

using System;
using Gilzoide.GestureRecognizers.Common;
using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    [Serializable]
    public class SwipeGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTouches = 1;
        [Min(0)] public float MinimumDistance = 1;
        [Min(0)] public float MinimumVelocity = 1000;
        public SwipeDirection SupportedDirections = SwipeDirection.Up;
        public TimeProvider TimeProvider = TimeProvider.UnscaledTime;

        [Space]
        public UnityEventSwipeGesture OnSwipeRecognized;

        public bool IsSwiping => TouchCount >= NumberOfTouches;
        public SwipeDirection Direction { get; protected set; } = SwipeDirection.None;

        protected Vector2 _initialPosition;
        protected float _initialTime;

        public override void TouchStarted(int touchId, Vector2 position)
        {
            if (IsSwiping)
            {
                return;
            }

            base.TouchStarted(touchId, position);
            if (IsSwiping)
            {
                _initialPosition = GetPosition();
                _initialTime = TimeProvider.GetTime();
            }
        }

        public override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);
            if (IsSwiping)
            {
                Vector2 positionDelta = GetPosition() - _initialPosition;
                float timeDelta = TimeProvider.GetTime() - _initialTime;
                SwipeDirection direction = SupportedDirections & GetSwipeDirection(positionDelta, timeDelta);
                if (direction != 0)
                {
                    Direction = direction;
                    OnSwipeRecognized.Invoke(new SwipeGesture
                    {
                        NumberOfTouches = NumberOfTouches,
                        InitialPosition = _initialPosition,
                        Vector = positionDelta,
                        Time = timeDelta,
                        SwipeDirection = Direction,
                    });
                }
            }
        }

        public override void TouchEnded(int touchId)
        {
            bool wasSwiping = IsSwiping;
            base.TouchEnded(touchId);
            if (!IsSwiping && wasSwiping)
            {
                Direction = SwipeDirection.None;
            }
        }

        protected Vector2 GetPosition()
        {
            return Centroid.Value;
        }

        public SwipeDirection GetSwipeDirection(Vector2 positionDelta, float timeDelta)
        {
            return GetSwipeDirection(positionDelta, timeDelta, MinimumDistance, MinimumVelocity);
        }

        public static SwipeDirection GetSwipeDirection(Vector2 positionDelta, float timeDelta, float distanceThreshold, float velocityThreshold)
        {
            float diffX = Mathf.Abs(positionDelta.x);
            float diffY = Mathf.Abs(positionDelta.y);
            if (diffX >= diffY)
            {
                if (diffX >= distanceThreshold && timeDelta > 0 && (diffX / timeDelta) >= velocityThreshold)
                {
                    return positionDelta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                }
            }
            else if (diffY >= distanceThreshold && timeDelta > 0 && (diffY / timeDelta) >= velocityThreshold)
            {
                return positionDelta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            }
            return SwipeDirection.None;
        }
    }
}

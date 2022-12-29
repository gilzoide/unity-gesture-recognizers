using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class SwipeGestureRecognizer : AGestureRecognizer
    {
        [Header("Options")]
        [Min(1)] public int NumberOfTouches = 1;
        [Min(0)] public float MinimumDistance = 50;
        [Min(0)] public float MinimumVelocity = 1000;
        public SwipeDirection SupportedDirections = SwipeDirection.Up;
        public TimeProvider TimeProvider = TimeProvider.UnscaledTime;

        [Header("Swipe events")]
        public UnityEventSwipeDirection OnSwipeRecognized;


        public bool IsSwiping => TouchCount >= NumberOfTouches;
        public SwipeDirection Direction { get; protected set; } = SwipeDirection.None;

        protected Vector2 _initialPosition;
        protected float _initialTime;

        protected override void TouchStarted(int touchId, Vector2 position)
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

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);
            if (IsSwiping)
            {
                SwipeDirection direction = GetSwipeDirection(
                    GetPosition() - _initialPosition,
                    TimeProvider.GetTime() - _initialTime,
                    MinimumDistance,
                    MinimumVelocity
                );
                if ((direction & SupportedDirections) != 0)
                {
                    Direction = direction;
                    OnGestureRecognized.Invoke();
                    OnSwipeRecognized.Invoke(direction);
                }
            }

        }

        protected override void TouchEnded(int touchId)
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

        public static SwipeDirection GetSwipeDirection(Vector2 vector, float time, float distanceThreshold, float velocityThreshold)
        {
            float diffX = Mathf.Abs(vector.x);
            float diffY = Mathf.Abs(vector.y);
            if (diffX >= diffY)
            {
                if (diffX >= distanceThreshold && time > 0 && (diffX / time) >= velocityThreshold)
                {
                    return vector.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                }
            }
            else if (diffY >= distanceThreshold && time > 0 && (diffY / time) >= velocityThreshold)
            {
                return vector.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            }
            return SwipeDirection.None;
        }
    }
}

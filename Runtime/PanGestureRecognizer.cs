using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class PanGestureRecognizer : AContinuousGestureRecognizer
    {
        [Min(1)] public int NumberOfTouches = 1;
        [Header("Pan events")]
        public UnityEventVector2 OnPositionChanged;
        public UnityEventVector2 OnPositionDelta;

        public bool IsPanning => TouchCount >= NumberOfTouches;
        
        protected Vector2 _initialCentroid;
        protected Vector2 _lastPosition;
        protected bool _firstMove;

        protected override void TouchStarted(int touchId, Vector2 position)
        {
            if (IsPanning)
            {
                return;
            }

            base.TouchStarted(touchId, position);
            if (IsPanning)
            {
                _initialCentroid = _lastPosition = Centroid.Value;
                _firstMove = true;
            }
        }

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);
            if (IsPanning)
            {
                if (_firstMove)
                {
                    _firstMove = false;
                    OnGestureRecognized.Invoke();
                }

                Vector2 currentPosition = Centroid.Value;
                OnPositionChanged.Invoke(currentPosition);
                OnPositionDelta.Invoke(currentPosition - _lastPosition);
                _lastPosition = currentPosition;
            }
        }

        protected override void TouchEnded(int touchId)
        {
            bool wasPanning = IsPanning;
            base.TouchEnded(touchId);
            if (!IsPanning && wasPanning)
            {
                _firstMove = false;
                OnGestureEnded.Invoke();
            }
        }
    }
}

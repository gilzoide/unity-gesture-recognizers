using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class GestureRecognizer : MonoBehaviour
    {
        public UnityEvent OnGestureRecognized;

        protected readonly TouchTracker _touchTracker = new TouchTracker();

        public int TouchCount => _touchTracker.Count;
        public ICollection<Vector2> TouchPositions => _touchTracker.TouchPositions;
        public Vector2? Centroid => _touchTracker.Centroid;
        public float? AverageDistanceToCentroid => _touchTracker.AverageDistanceToCentroid;

        protected virtual void TouchStarted(int touchId, Vector2 position)
        {
            _touchTracker.TouchStarted(touchId, position);
        }

        protected virtual void TouchMoved(int touchId, Vector2 position)
        {
            _touchTracker.TouchMoved(touchId, position);
        }

        protected virtual void TouchEnded(int touchId)
        {
            _touchTracker.TouchEnded(touchId);
        }
    }
}

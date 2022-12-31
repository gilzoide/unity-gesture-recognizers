using System;
using System.Collections.Generic;
using Gilzoide.GestureRecognizers.Common;
using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    [Serializable]
    public abstract class AGestureRecognizer
    {
        public int TouchCount => _touchTracker.Count;
        public ICollection<Vector2> TouchPositions => _touchTracker.TouchPositions;
        public Vector2? Centroid => _touchTracker.Centroid;
        public float? AverageDistanceToCentroid => _touchTracker.AverageDistanceToCentroid;

        protected readonly TouchTracker _touchTracker = new TouchTracker();

        public virtual void TouchStarted(int touchId, Vector2 position)
        {
            _touchTracker.TouchStarted(touchId, position);
        }

        public virtual void TouchMoved(int touchId, Vector2 position)
        {
            _touchTracker.TouchMoved(touchId, position);
        }

        public virtual void TouchEnded(int touchId)
        {
            _touchTracker.TouchEnded(touchId);
        }
    }
}

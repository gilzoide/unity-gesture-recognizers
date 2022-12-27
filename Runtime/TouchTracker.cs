using System.Collections.Generic;
using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class TouchTracker
    {
        protected Dictionary<int, Vector2> _touchPositions = new Dictionary<int, Vector2>();

        public Vector2? Centroid => FindCentroid();
        public int Count => _touchPositions.Count;

        public void TouchStarted(int touchId, Vector2 position)
        {
            _touchPositions[touchId] = position;
        }

        public void TouchMoved(int touchId, Vector2 position)
        {
            _touchPositions[touchId] = position;
        }

        public void TouchEnded(int touchId)
        {
            _touchPositions.Remove(touchId);
        }

        protected Vector2? FindCentroid()
        {
            if (_touchPositions.Count == 0)
            {
                return null;
            }

            Vector2 centroid = Vector2.zero;
            foreach (Vector2 position in _touchPositions.Values)
            {
                centroid += position;
            }
            return centroid / _touchPositions.Count;
        }
    }
}

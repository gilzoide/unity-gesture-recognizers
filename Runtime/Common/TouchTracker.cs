using System.Collections.Generic;
using UnityEngine;

namespace Gilzoide.GestureRecognizers.Common
{
    public class TouchTracker
    {
        protected Dictionary<int, Vector2> _touchPositions = new Dictionary<int, Vector2>();

        public int Count => _touchPositions.Count;
        public ICollection<Vector2> TouchPositions => _touchPositions.Values;
        public Vector2? Centroid => FindCentroid();
        public float? AverageDistanceToCentroid => FindAverageDistanceToCentroid();

        public void TouchStarted(int touchId, Vector2 position)
        {
            _touchPositions[touchId] = position;
        }

        public void TouchMoved(int touchId, Vector2 position)
        {
            if (_touchPositions.ContainsKey(touchId))
            {
                _touchPositions[touchId] = position;
            }
        }

        public void TouchEnded(int touchId)
        {
            _touchPositions.Remove(touchId);
        }

        public IEnumerable<Vector2> EnumerateTouchVectors()
        {
            if (Count == 0)
            {
                yield break;
            }

            Vector2 centroid = Centroid.Value;
            foreach (Vector2 position in TouchPositions)
            {
                yield return position - centroid;
            }
        }

        protected Vector2? FindCentroid()
        {
            if (Count == 0)
            {
                return null;
            }

            Vector2 centroid = Vector2.zero;
            foreach (Vector2 position in TouchPositions)
            {
                centroid += position;
            }
            return centroid / Count;
        }

        protected float? FindAverageDistanceToCentroid()
        {
            if (Count == 0)
            {
                return null;
            }

            Vector2 centroid = Centroid.Value;
            float totalDistance = 0;
            foreach (Vector2 position in TouchPositions)
            {
                totalDistance += Vector2.Distance(position, centroid);
            }
            return totalDistance / Count;
        }
    }
}

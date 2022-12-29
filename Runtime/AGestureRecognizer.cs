using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Gilzoide.GestureRecognizers
{
    public abstract class AGestureRecognizer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IInitializePotentialDragHandler, IDragHandler
    {
        [Header("General gesture events")]
        public UnityEvent OnGestureRecognized;

        protected readonly TouchTracker _touchTracker = new TouchTracker();

        public int TouchCount => _touchTracker.Count;
        public ICollection<Vector2> TouchPositions => _touchTracker.TouchPositions;
        public Vector2? Centroid => _touchTracker.Centroid;
        public float? AverageDistanceToCentroid => _touchTracker.AverageDistanceToCentroid;

        void Start()
        {
            // No-op, but it's here so we can disable the scripts via inspector
            // Disabled scripts don't receive pointer events
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TouchStarted(eventData.pointerId, eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchEnded(eventData.pointerId);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            eventData.useDragThreshold = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            TouchMoved(eventData.pointerId, eventData.position);
        }

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

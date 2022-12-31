using UnityEngine;
using UnityEngine.EventSystems;

namespace Gilzoide.GestureRecognizers.EventSystems
{
    public abstract class AEventSystemGestureRecognizer<T> : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler, IInitializePotentialDragHandler, IDragHandler
        where T : AGestureRecognizer, new()
    {
        public T GestureRecognizer = new T();

        protected virtual void Start()
        {
            // No-op, but it's here so we can disable the scripts via inspector
            // Disabled scripts don't receive pointer events
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GestureRecognizer.TouchStarted(eventData.pointerId, eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GestureRecognizer.TouchEnded(eventData.pointerId);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            eventData.useDragThreshold = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            GestureRecognizer.TouchMoved(eventData.pointerId, eventData.position);
        }
    }
}

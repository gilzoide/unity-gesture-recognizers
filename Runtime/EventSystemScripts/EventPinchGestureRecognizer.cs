using UnityEngine.EventSystems;

namespace Gilzoide.GestureRecognizers
{
    public class EventPinchGestureRecognizer : PinchGestureRecognizer, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            TouchStarted(eventData.pointerId, eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchEnded(eventData.pointerId);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TouchMoved(eventData.pointerId, eventData.position);
        }
    }
}

using UnityEngine.EventSystems;

namespace Gilzoide.GestureRecognizers.EventSystemScripts
{
    public class EventTapGestureRecognizer : TapGestureRecognizer, IPointerDownHandler, IPointerUpHandler
    {   
        public void OnPointerDown(PointerEventData eventData)
        {
            TouchStarted(eventData.pointerId, eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchEnded(eventData.pointerId);
        }
    }
}

using Gilzoide.GestureRecognizers.Recognizers;
using UnityEngine;

namespace Gilzoide.GestureRecognizers.Input
{
    public abstract class AInputGestureRecognizer<T> : MonoBehaviour
        where T : AGestureRecognizer, new()
    {
        public Rect ViewportRect = new Rect(0, 0, 1, 1);
        public T GestureRecognizer = new T();

        protected virtual void Update()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            for (int i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                Touch touch = UnityEngine.Input.GetTouch(i);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (ViewportRect.Contains(touch.position / screenSize))
                        {
                            GestureRecognizer.TouchStarted(touch.fingerId, touch.position);
                        }
                        break;
                    
                    case TouchPhase.Moved:
                        GestureRecognizer.TouchMoved(touch.fingerId, touch.position);
                        break;
                    
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        GestureRecognizer.TouchEnded(touch.fingerId);
                        break;
                }
            }
        }
    }
}

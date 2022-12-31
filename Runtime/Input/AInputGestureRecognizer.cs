using UnityEngine;

namespace Gilzoide.GestureRecognizers.Input
{
    public abstract class AInputGestureRecognizer<T> : MonoBehaviour
        where T : AGestureRecognizer, new()
    {
        public Rect ViewportRect = new Rect(0, 0, 1, 1);
        public T GestureRecognizer = new T();

        protected Vector2 _lastMousePosition;

        protected virtual void Start()
        {
            _lastMousePosition = UnityEngine.Input.mousePosition;
        }

        protected virtual void Update()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            Vector2 mousePosition = UnityEngine.Input.mousePosition;
            bool mouseMoved = mousePosition != _lastMousePosition;
            for (int i = 0; i < 3; i++)
            {
                int touchId = MouseButtonToTouchId(i);
                if (UnityEngine.Input.GetMouseButtonDown(i))
                {
                    if (ViewportRect.Contains(mousePosition / screenSize))
                    {
                        GestureRecognizer.TouchStarted(touchId, mousePosition);
                    }
                }
                else if (UnityEngine.Input.GetMouseButtonUp(i))
                {
                    GestureRecognizer.TouchEnded(touchId);
                }
                else if (mouseMoved && UnityEngine.Input.GetMouseButton(i))
                {
                    GestureRecognizer.TouchMoved(touchId, mousePosition);
                }
            }
            _lastMousePosition = mousePosition;

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

        protected int MouseButtonToTouchId(int index)
        {
            return -index - 1;
        }
    }
}

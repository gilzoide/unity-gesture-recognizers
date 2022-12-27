using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class LongPressGestureRecognizer : MonoBehaviour
    {
        [Min(1)] public int NumberOfTouchesRequired = 1;
        [Min(float.Epsilon)] public float PressDuration = 1f;
        public float AllowableMovement = 10;
        public UnityEvent OnGestureRecognized;

        protected readonly TouchTracker _touchTracker = new TouchTracker();
        protected Coroutine _pressCoroutine;
        protected Vector2 _initialPosition;

        public Vector2? Position => _touchTracker.Centroid;

        public void Cancel()
        {
            if (_pressCoroutine != null)
            {
                StopCoroutine(_pressCoroutine);
            }
        }

        protected void TouchStarted(int touchId, Vector2 position)
        {
            _touchTracker.TouchStarted(touchId, position);
            
            if (_touchTracker.Count == NumberOfTouchesRequired)
            {
                _initialPosition = _touchTracker.Centroid.Value;
                _pressCoroutine = StartCoroutine(RecognizePress());
            }
        }

        protected void TouchMoved(int touchId, Vector2 position)
        {
            _touchTracker.TouchMoved(touchId, position);

            if (_touchTracker.Count >= NumberOfTouchesRequired && Vector2.Distance(_initialPosition, _touchTracker.Centroid.Value) > AllowableMovement)
            {
                Cancel();
            }
        }

        protected void TouchEnded(int touchId)
        {
            _touchTracker.TouchEnded(touchId);

            if (_touchTracker.Count < NumberOfTouchesRequired)
            {
                Cancel();
            }
        }

        protected IEnumerator RecognizePress()
        {
            yield return new WaitForSecondsRealtime(PressDuration);
            OnGestureRecognized.Invoke();
        }
    }
}

using System.Collections;
using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public class LongPressGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTouchesRequired = 1;
        [Min(float.Epsilon)] public float PressDuration = 1f;
        public float AllowableMovement = 10;

        protected Coroutine _pressCoroutine;
        protected Vector2 _initialPosition;

        public void Cancel()
        {
            if (_pressCoroutine != null)
            {
                StopCoroutine(_pressCoroutine);
            }
        }

        protected override void TouchStarted(int touchId, Vector2 position)
        {
            base.TouchStarted(touchId, position);

            if (TouchCount == NumberOfTouchesRequired)
            {
                _initialPosition = Centroid.Value;
                _pressCoroutine = StartCoroutine(RecognizePress());
            }
        }

        protected override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);

            if (TouchCount >= NumberOfTouchesRequired && Vector2.Distance(_initialPosition, Centroid.Value) > AllowableMovement)
            {
                Cancel();
            }
        }

        protected override void TouchEnded(int touchId)
        {
            base.TouchEnded(touchId);

            if (TouchCount < NumberOfTouchesRequired)
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

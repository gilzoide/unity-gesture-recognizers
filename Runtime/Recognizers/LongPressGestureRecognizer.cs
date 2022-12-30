using System;
using System.Collections;
using Gilzoide.GestureRecognizers.Recognizers.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers.Recognizers
{
    [Serializable]
    public class LongPressGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTouches = 1;
        [Min(float.Epsilon)] public float PressDuration = 1f;
        public float AllowableMovement = 10;
        public TimeProvider TimeProvider = TimeProvider.UnscaledTime;

        [Space]
        public UnityEvent OnLongPressRecognized;

        [HideInInspector] public MonoBehaviour CoroutineRunner { get; set; }

        protected Coroutine _pressCoroutine;
        protected Vector2 _initialPosition;

        public void Cancel()
        {
            if (_pressCoroutine != null)
            {
                CoroutineRunner.StopCoroutine(_pressCoroutine);
            }
        }

        public override void TouchStarted(int touchId, Vector2 position)
        {
            base.TouchStarted(touchId, position);

            if (TouchCount == NumberOfTouches)
            {
                _initialPosition = Centroid.Value;
                _pressCoroutine = CoroutineRunner.StartCoroutine(RecognizePress());
            }
        }

        public override void TouchMoved(int touchId, Vector2 position)
        {
            base.TouchMoved(touchId, position);

            if (TouchCount >= NumberOfTouches && Vector2.Distance(_initialPosition, Centroid.Value) > AllowableMovement)
            {
                Cancel();
            }
        }

        public override void TouchEnded(int touchId)
        {
            base.TouchEnded(touchId);

            if (TouchCount < NumberOfTouches)
            {
                Cancel();
            }
        }

        protected IEnumerator RecognizePress()
        {
            yield return TimeProvider.WaitForSeconds(PressDuration);
            OnLongPressRecognized.Invoke();
        }
    }
}

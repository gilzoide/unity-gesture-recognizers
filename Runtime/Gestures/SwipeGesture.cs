using Gilzoide.GestureRecognizers.Common;
using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public struct SwipeGesture
    {
        public int NumberOfTouches;
        public Vector2 InitialPosition;
        public Vector2 Vector;
        public float Time;
        public SwipeDirection SwipeDirection;

        public Vector2 Direction => Vector.normalized;
        public float Distance => Vector.magnitude;
        public float Velocity => Time > 0 ? Distance / Time : 0;
    }
}

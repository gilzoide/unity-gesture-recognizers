using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public struct PinchGesture
    {
        public int NumberOfTouches;
        public Vector2 Center;
        public float Scale;
        public float Delta;
    }
}

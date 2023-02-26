using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public struct TwistGesture
    {
        public int NumberOfTouches;
        public Vector2 Center;
        public float Rotation;
        public float Delta;
    }
}

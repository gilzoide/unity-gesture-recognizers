using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public struct PanGesture
    {
        public int NumberOfTouches;
        public Vector2 InitialPosition;
        public Vector2 Position;
        public Vector2 Delta;
    }
}

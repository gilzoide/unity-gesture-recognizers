using Gilzoide.GestureRecognizers.Common;
using UnityEngine;

namespace Gilzoide.GestureRecognizers
{
    public struct EdgePanGesture
    {
        public int NumberOfTouches;
        public Vector2 InitialPosition;
        public Vector2 Position;
        public Vector2 Delta;
        public RectEdge Edge;
    }
}

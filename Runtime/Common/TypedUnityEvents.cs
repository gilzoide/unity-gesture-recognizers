using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers.Common
{
    [Serializable] public class UnityEventVector2 : UnityEvent<Vector2> {}
    [Serializable] public class UnityEventFloat : UnityEvent<float> {}
    [Serializable] public class UnityEventSwipeDirection : UnityEvent<SwipeDirection> {}
    [Serializable] public class UnityEventRectEdge : UnityEvent<RectEdge> {}
}

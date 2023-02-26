using System;
using Gilzoide.GestureRecognizers.Gestures;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers.Common
{
    // primitives
    [Serializable] public class UnityEventFloat : UnityEvent<float> {}
    [Serializable] public class UnityEventVector2 : UnityEvent<Vector2> {}
    
    // enums
    [Serializable] public class UnityEventSwipeDirection : UnityEvent<SwipeDirection> {}
    [Serializable] public class UnityEventRectEdge : UnityEvent<RectEdge> {}
    
    // gesture structs
    [Serializable] public class UnityEventTapGesture : UnityEvent<TapGesture> {}
    [Serializable] public class UnityEventLongPressGesture : UnityEvent<LongPressGesture> {}
    [Serializable] public class UnityEventPanGesture : UnityEvent<PanGesture> {}
}

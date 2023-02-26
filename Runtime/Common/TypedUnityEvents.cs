using System;
using Gilzoide.GestureRecognizers.Gestures;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers.Common
{
    [Serializable] public class UnityEventTapGesture : UnityEvent<TapGesture> {}
    [Serializable] public class UnityEventLongPressGesture : UnityEvent<LongPressGesture> {}
    [Serializable] public class UnityEventPanGesture : UnityEvent<PanGesture> {}
    [Serializable] public class UnityEventEdgePanGesture : UnityEvent<EdgePanGesture> {}
    [Serializable] public class UnityEventPinchGesture : UnityEvent<PinchGesture> {}
    [Serializable] public class UnityEventTwistGesture : UnityEvent<TwistGesture> {}
    [Serializable] public class UnityEventSwipeGesture : UnityEvent<SwipeGesture> {}
}

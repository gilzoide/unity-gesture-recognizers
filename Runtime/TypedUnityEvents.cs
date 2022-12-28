using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    [Serializable] public class UnityEventVector2 : UnityEvent<Vector2> {}
    [Serializable] public class UnityEventFloat : UnityEvent<float> {}
}
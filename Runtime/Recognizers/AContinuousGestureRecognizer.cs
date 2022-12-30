using System;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers.Recognizers
{
    [Serializable]
    public abstract class AContinuousGestureRecognizer : AGestureRecognizer
    {
        public UnityEvent OnGestureEnded;
    }
}

using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class AContinuousGestureRecognizer : AGestureRecognizer
    {
        public UnityEvent OnGestureEnded;
    }
}

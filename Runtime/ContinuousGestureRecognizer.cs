using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    public abstract class ContinuousGestureRecognizer : GestureRecognizer
    {
        public UnityEvent OnGestureEnded;
    }
}

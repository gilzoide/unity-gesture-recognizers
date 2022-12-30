using Gilzoide.GestureRecognizers.Recognizers;

namespace Gilzoide.GestureRecognizers.EventSystems
{
    public class LongPressRecognizer : AEventSystemGestureRecognizer<LongPressGestureRecognizer>
    {
        protected virtual void Awake()
        {
            GestureRecognizer.CoroutineRunner = this;
        }
    }
}

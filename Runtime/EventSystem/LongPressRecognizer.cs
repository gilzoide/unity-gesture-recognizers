namespace Gilzoide.GestureRecognizers.EventSystem
{
    public class LongPressRecognizer : AEventSystemGestureRecognizer<LongPressGestureRecognizer>
    {
        protected virtual void Awake()
        {
            GestureRecognizer.CoroutineRunner = this;
        }
    }
}

using Gilzoide.GestureRecognizers.Recognizers;

namespace Gilzoide.GestureRecognizers.Input
{
    public class ScreenLongPressRecognizer : AInputGestureRecognizer<LongPressGestureRecognizer>
    {
        protected virtual void Awake()
        {
            GestureRecognizer.CoroutineRunner = this;
        }
    }
}

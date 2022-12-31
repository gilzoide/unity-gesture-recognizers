using UnityEngine;

namespace Gilzoide.GestureRecognizers.Input
{
    public class ScreenEdgePanRecognizer : AInputGestureRecognizer<EdgePanGestureRecognizer>
    {
        protected override void Update()
        {
            GestureRecognizer.Rect = new Rect(0, 0, Screen.width, Screen.height);
            base.Update();
        }
    }
}

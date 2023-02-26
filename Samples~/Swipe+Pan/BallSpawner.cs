using UnityEngine;

namespace Gilzoide.GestureRecognizers.Samples.SwipePan
{
    public class BallSpawner : MonoBehaviour
    {
        public Ball BallPrefab;
        public RectTransform BallParent;

        private Ball _ball;

        public void OnSwipe(SwipeGesture swipe)
        {
            if (_ball)
            {
                return;
            }

            _ball = Instantiate(BallPrefab, swipe.InitialPosition, Quaternion.identity, BallParent);
            _ball.Setup(swipe.Vector / swipe.Time);
        }
    }
}

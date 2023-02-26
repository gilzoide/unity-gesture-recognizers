using UnityEngine;

namespace Gilzoide.GestureRecognizers.Samples.SwipePan
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody;

        public float MaxVelocity = 2500;

        public void Setup(Vector2 vector)
        {
            _rigidBody.velocity = Vector2.ClampMagnitude(vector, MaxVelocity);
        }

        public void Update()
        {
            Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
            if (!screenRect.Contains(_rigidBody.position))
            {
                Destroy(gameObject);
            }
        }
    }
}

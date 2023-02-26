using UnityEngine;

namespace Gilzoide.GestureRecognizers.Samples.SwipePan
{
    public class GoalKeeper : MonoBehaviour
    {
        [SerializeField] private RectTransform _goalArea;
        [SerializeField] private Rigidbody2D _rigidBody;

        private Vector2 _newPosition;

        void Start()
        {
            _newPosition = _rigidBody.position;
        }

        void FixedUpdate()
        {
            _rigidBody.MovePosition(_newPosition);
        }

        public void HandlePanGesture(PanGesture pan)
        {
            Vector2 position = _goalArea.InverseTransformPoint(pan.Position);
            Rect goalRect = _goalArea.rect;
            _newPosition = _goalArea.TransformPoint(Vector2.Max(goalRect.min, Vector2.Min(goalRect.max, position)));
        }
    }
}


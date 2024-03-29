using System;
using Gilzoide.GestureRecognizers.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.GestureRecognizers
{
    [Serializable]
    public class EdgePanGestureRecognizer : AGestureRecognizer
    {
        [Min(1)] public int NumberOfTouches = 1;
        [Min(0.0001f)] public float EdgeThreshold = 50;
        public RectEdge SupportedEdges = RectEdge.Left;

        [Space]
        public UnityEventEdgePanGesture OnEdgePanStarted = new UnityEventEdgePanGesture();
        public UnityEventEdgePanGesture OnEdgePanRecognized = new UnityEventEdgePanGesture();
        public UnityEvent OnGestureEnded = new UnityEvent();

        public Rect Rect { get; set; }

        public bool IsPanning => TouchCount >= NumberOfTouches;
        public RectEdge Edge => IsPanning ? _possibleEdges : RectEdge.None;
        public Vector2 Position => IsPanning ? GetPosition() : Vector2.zero;
        
        protected bool _firstMove;
        protected Vector2 _initialPosition;
        protected RectEdge _possibleEdges = RectEdge.None;

        public override void TouchStarted(int touchId, Vector2 position)
        {
            if (IsPanning)
            {
                return;
            }

            if (_possibleEdges == RectEdge.None)
            {
                _possibleEdges = SupportedEdges;
            }

            RectEdge pointerEdge = GetRectEdgeForPosition(position);
            if ((pointerEdge & _possibleEdges) == 0)
            {
                return;
            }

            _possibleEdges &= pointerEdge;
            base.TouchStarted(touchId, position);
            if (IsPanning)
            {
                _firstMove = true;
            }
        }

        public override void TouchMoved(int touchId, Vector2 position)
        {
            if (!IsPanning)
            {
                base.TouchMoved(touchId, position);
                return;
            }

            Vector2 previousPosition = GetPosition();

            if (_firstMove)
            {
                _firstMove = false;
                _initialPosition = previousPosition;
                OnEdgePanStarted.Invoke(new EdgePanGesture
                {
                    NumberOfTouches = NumberOfTouches,
                    InitialPosition = _initialPosition,
                    Position = previousPosition,
                    Delta = Vector2.zero,
                    Edge = _possibleEdges,
                });
            }
            
            base.TouchMoved(touchId, position);

            Vector2 currentPosition = GetPosition();
            OnEdgePanRecognized.Invoke(new EdgePanGesture
            {
                NumberOfTouches = NumberOfTouches,
                InitialPosition = _initialPosition,
                Position = currentPosition,
                Delta = currentPosition - previousPosition,
                Edge = _possibleEdges,
            });
        }

        public override void TouchEnded(int touchId)
        {
            bool wasPanning = IsPanning;
            base.TouchEnded(touchId);
            if (!IsPanning && wasPanning)
            {
                _firstMove = false;
                _possibleEdges = SupportedEdges;
                OnGestureEnded.Invoke();
            }
        }

        public RectEdge GetRectEdgeForPosition(Vector2 position)
        {
            RectEdge edge = RectEdge.None;

            if (position.x >= Rect.xMin && position.x <= Rect.xMin + EdgeThreshold)
            {
                edge |= RectEdge.Left;
            }
            if (position.x <= Rect.xMax && position.x >= Rect.xMax - EdgeThreshold)
            {
                edge |= RectEdge.Right;
            }

            if (position.y >= Rect.yMin && position.y <= Rect.yMin + EdgeThreshold)
            {
                edge |= RectEdge.Bottom;
            }
            if (position.y <= Rect.yMax && position.y >= Rect.yMax - EdgeThreshold)
            {
                edge |= RectEdge.Top;
            }

            return edge;
        }

        protected Vector2 GetPosition()
        {
            return Centroid.Value;
        }
    }
}

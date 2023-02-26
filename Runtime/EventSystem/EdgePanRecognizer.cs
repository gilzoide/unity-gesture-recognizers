using UnityEngine;

namespace Gilzoide.GestureRecognizers.EventSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class EdgePanRecognizer : AEventSystemGestureRecognizer<EdgePanGestureRecognizer>
    {
        protected Canvas _canvas;
        protected readonly Vector3[] _worldCorners = new Vector3[4];

        protected virtual void OnEnable()
        {
            _canvas = FindRootCanvas();
            RefreshRect();
        }

        protected virtual void OnTransformParentChanged()
        {
            if (isActiveAndEnabled)
            {
                _canvas = FindRootCanvas();
                RefreshRect();
            }
        }

        protected virtual void Update()
        {
            if (transform.hasChanged)
            {
                RefreshRect();
            }
        }

        protected virtual void LateUpdate()
        {
            transform.hasChanged = false;
        }

        protected void RefreshRect()
        {
            GestureRecognizer.Rect = GetScreenRect();
        }

        protected Rect GetScreenRect()
        {
            ((RectTransform) transform).GetWorldCorners(_worldCorners);

            Vector3 bottomLeft = _worldCorners[0];
            Vector3 topRight = _worldCorners[2];
            if (_canvas && _canvas.renderMode == RenderMode.ScreenSpaceCamera && _canvas.worldCamera != null)
            {
                Camera camera = _canvas.worldCamera;
                bottomLeft = camera.WorldToScreenPoint(bottomLeft);
                topRight = camera.WorldToScreenPoint(topRight);
            }
            return Rect.MinMaxRect(bottomLeft.x, bottomLeft.y, topRight.x, topRight.y);
        }

        protected Canvas FindRootCanvas()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            return canvas != null ? canvas.rootCanvas : null;
        }
    }
}

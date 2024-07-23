using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class CanvasService
    {
        private Canvas _overlayCanvas;

        [Inject]
        public void Inject(Canvas overlayCanvas) => _overlayCanvas = overlayCanvas;

        public void MoveToOverlay(RectTransform element)
        {
            if (element == null) return;

            element.SetParent(_overlayCanvas.transform, false);
            element.gameObject.SetActive(true);
        }

        public void MoveBack(RectTransform element, Transform originalParent)
        {
            if (element == null || originalParent == null) return;

            element.SetParent(originalParent, false);
        }
    }
}
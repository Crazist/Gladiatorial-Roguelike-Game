using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class CanvasService
    {
        private Canvas _overlayCanvas;
        private Dictionary<RectTransform, Transform> _originalParents = new Dictionary<RectTransform, Transform>();

        [Inject]
        public void Inject(Canvas overlayCanvas) => _overlayCanvas = overlayCanvas;

        public void MoveToOverlay(RectTransform element)
        {
            if (element == null) return;

            if (!_originalParents.ContainsKey(element))
            {
                _originalParents[element] = element.parent;
            }

            element.SetParent(_overlayCanvas.transform, false);
            element.gameObject.SetActive(true);
        }

        public void MoveBack(RectTransform element)
        {
            if (element == null) return;

            if (_originalParents.TryGetValue(element, out Transform originalParent))
            {
                element.SetParent(originalParent, false);
                _originalParents.Remove(element);
            }
        }

        public Canvas GetOverlayCanvas() => _overlayCanvas;
    }
}
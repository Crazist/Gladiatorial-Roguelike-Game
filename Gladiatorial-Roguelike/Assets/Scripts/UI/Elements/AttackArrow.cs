using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class AttackArrow : MonoBehaviour
    {
        [SerializeField] private Image _arrowImage;

        public void SetPositions(Vector3 start, Vector3 end)
        {
            Vector3 direction = end - start;
            _arrowImage.transform.position = start;
            _arrowImage.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            _arrowImage.rectTransform.sizeDelta = new Vector2(_arrowImage.rectTransform.sizeDelta.x, direction.magnitude);
        }
    }
}
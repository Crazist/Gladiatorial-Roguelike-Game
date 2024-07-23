using UnityEngine;
using UnityEngine.UI;

public class LineRendererUi : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private RectTransform _mMyTransform;
    [SerializeField] private Image _mImage;
    private Camera mainCamera;

    private void Start() => mainCamera = Camera.main;

    public void CreateLine(Vector3 start, Vector3 end, Color color)
    {
        _mImage.color = color;

        Vector2 screenPointStart = RectTransformUtility.WorldToScreenPoint(mainCamera, start);
        Vector2 screenPointEnd = RectTransformUtility.WorldToScreenPoint(mainCamera, end);

        Vector2 localPointStart;
        Vector2 localPointEnd;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_mMyTransform.parent as RectTransform, screenPointStart, mainCamera, out localPointStart);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_mMyTransform.parent as RectTransform, screenPointEnd, mainCamera, out localPointEnd);

        Vector2 midpoint = (localPointStart + localPointEnd) / 2f;
        _mMyTransform.localPosition = midpoint;

        Vector2 direction = localPointEnd - localPointStart;
        _mMyTransform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        _mMyTransform.sizeDelta = new Vector2(direction.magnitude, _mMyTransform.sizeDelta.y);
    }

    public void SetLineActive(bool active) => _mImage.gameObject.SetActive(active);
    public RectTransform GetRectTransform() => _transform;
}
using UnityEngine;
using UnityEngine.UI;

public class LineRendererUi : MonoBehaviour
{
    [SerializeField] private RectTransform m_myTransform;
    [SerializeField] private Image m_image;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void CreateLine(Vector3 start, Vector3 end, Color color)
    {
        m_image.color = color;

        Vector2 screenPointStart = RectTransformUtility.WorldToScreenPoint(mainCamera, start);
        Vector2 screenPointEnd = RectTransformUtility.WorldToScreenPoint(mainCamera, end);

        Vector2 localPointStart;
        Vector2 localPointEnd;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_myTransform.parent as RectTransform, screenPointStart, mainCamera, out localPointStart);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_myTransform.parent as RectTransform, screenPointEnd, mainCamera, out localPointEnd);

        Vector2 midpoint = (localPointStart + localPointEnd) / 2f;
        m_myTransform.localPosition = midpoint;

        Vector2 direction = localPointEnd - localPointStart;
        m_myTransform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        m_myTransform.sizeDelta = new Vector2(direction.magnitude, m_myTransform.sizeDelta.y);
    }

    public void SetLineActive(bool active)
    {
        m_image.gameObject.SetActive(active);
    }
}
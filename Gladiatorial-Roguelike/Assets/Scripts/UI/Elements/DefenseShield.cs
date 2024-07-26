using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class DefenseShield : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;

        public void SetText(string count)
        {
            _countText.text = count;
            gameObject.SetActive(true);
        }

        public void Hide() =>
            gameObject.SetActive(false);
    }
}
using TMPro;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _turnText;

    public void SetTurnText(string text) => 
        _turnText.text = text;
}
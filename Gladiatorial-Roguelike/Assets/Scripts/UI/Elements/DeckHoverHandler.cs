using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<Vector3> OnHoverEnter;
    public event Action<Vector3> OnHoverExit;

    public void OnPointerEnter(PointerEventData eventData) => 
        OnHoverEnter?.Invoke(transform.position);

    public void OnPointerExit(PointerEventData eventData) => 
        OnHoverExit?.Invoke(transform.position);
}
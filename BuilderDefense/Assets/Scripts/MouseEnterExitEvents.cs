using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler MouseEnter;
    public event EventHandler MouseExit;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit();
    }

    protected virtual void OnMouseEnter()
    {
        MouseEnter?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnMouseExit()
    {
        MouseExit?.Invoke(this, EventArgs.Empty);
    }
}
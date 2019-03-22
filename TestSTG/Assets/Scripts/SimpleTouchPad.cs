using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 origins;
    private Vector2 direction;

    public void OnDrag(PointerEventData eventData)
    {
        origins = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 currentPosition = eventData.position;
        Vector2 directionRaw = currentPosition - origins;
        direction = directionRaw.normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction = Vector2.zero;
    }

    private void Awake()
    {
        direction = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}

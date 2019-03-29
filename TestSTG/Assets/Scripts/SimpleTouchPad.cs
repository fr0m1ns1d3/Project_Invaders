using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler
{
    private Vector2 origins;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private Vector2 currentPosition;
    private bool touched;
    private int pointerID;

    public float smoothing;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            currentPosition = eventData.position;
            Vector2 directionRaw = currentPosition - origins;
            direction = directionRaw.normalized;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        origins = currentPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            pointerID = eventData.pointerId;
            origins = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            direction = Vector2.zero;
            touched = false;
        }
    }

    private void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

}

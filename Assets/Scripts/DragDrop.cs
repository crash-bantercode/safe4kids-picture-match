using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// Represents the card being dragged
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform startParent;
    private Transform currentParent;

    public void Awake()
    {
        // get properties and parent of the object
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startParent = GetComponent<Transform>().parent;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        // set the current parent to where we started drag from
        currentParent = startParent;
        // turn the dragging item transparent
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Make the card follow the event data for the drag - / by scalefactor keeps it relative to game dimensions
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // not being dragged anymore
        DragHandler.itemBeingDragged = null;
        // if the drag handler has not set a new parent for the card then send it back to orig position
        if (this.transform.parent == startParent)
        {
            rectTransform.position = startParent.position;
        }
        // non-transparent again
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}

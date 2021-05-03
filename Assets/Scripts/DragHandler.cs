using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged; // attach the card object
    // placeholders for starting position variables
    Transform startParent; 
    Vector3 startPosition;
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        // record what we need to move the object back to where it started
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        Debug.Log(itemBeingDragged.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // follow the mouse/drag event pos
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        //If the parent of the dragged object has not been changed(i.e. was dropped outside of any slot), bring it back
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
    }
}

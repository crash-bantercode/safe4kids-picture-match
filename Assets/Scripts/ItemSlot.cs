using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DragDrop;

public class ItemSlot : MonoBehaviour, IDropHandler, IEndDragHandler
{
    private Picture picture;
    public string dragName;
    private GameManager gameManager;

    void Awake()
    {
        // get access to game manager (up 2 parent levels)
        gameManager = this.transform.parent.transform.parent.GetComponent<GameManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        bool match = false;

        if (eventData.pointerDrag != null)
        {
            // sets the parent of the dropped image to the slot it was dropped on
            eventData.pointerDrag.GetComponent<Transform>().SetParent(GetComponent<Transform>().transform);
            // position relative to slot (snap it in)
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position - new Vector3(0, 30, 0);
            string imageName = eventData.pointerDrag.GetComponent<Picture>().getName();
            string slotName = this.transform.name;
            // if the game manager returns a match - call its next round method with the result
            if (gameManager.isMatch(imageName, slotName))
            {
                match = true;
                gameManager.nextRound(match, eventData.pointerDrag);
            } else
            {
                match = false;
                gameManager.nextRound(match, eventData.pointerDrag);
            }
        }
    }

    // I think the drop handler goes funny if this method isn't implented - could be wrong but leave for now as it works just fine
    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void setPicture(Picture pic)
    {
        // sets the picture proprty of this slot to our dragged picture
        this.picture = pic;
    }

    // check this is still used
    private IEnumerator waitABit(int secs)
    {
            yield return new WaitForSeconds(secs);
    }
}

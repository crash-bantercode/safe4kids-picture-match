using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{

    public GameManager gameManager;

    public void loadImage(Sprite img)
    {
        this.gameObject.GetComponent<Image>().sprite = img;
    } 

    public string getName()
    {
        return this.gameObject.GetComponent<Image>().sprite.name;
    }
}
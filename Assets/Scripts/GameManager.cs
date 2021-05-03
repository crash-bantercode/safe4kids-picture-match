using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // placeholders
    public Picture picture;
    public ItemSlot startSlot;
    public Text scoreText;
    private string difficulty;
    private Sprite[] imgArray;
    private Sprite[] shuffledImgArray;
    public Settings settings;
    private int matchCount;
    private AudioSource audio_source;
    private AudioClip sound;
    public Image frame;
    public Image tick;

    private void Awake()
    {
        audio_source = gameObject.AddComponent<AudioSource>();
        if (GameObject.Find("Settings") != null)
        {
            // get the difficulty from settings
            settings = GameObject.Find("Settings").GetComponent<Settings>();
            difficulty = settings.getDifficulty();
        } else
        {
            // if something goes wrong, fallback to easy - this needs fixing <<<<<<<
            difficulty = "easy";
        }
        // get our images from the resources folder
        imgArray = (Resources.LoadAll<Sprite>("Graphics/Pictures"));
        // shuffle them, if easy setting take only 10
        shuffledImgArray = shuffle((Sprite[])imgArray.Clone());
        if (difficulty == "easy") {
            Array.Resize(ref shuffledImgArray, 10);
        }
        matchCount = shuffledImgArray.Length;
        getNext();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Pictures left: " + matchCount;
    }

    // getNextImg
    public void getNext()
    {
        // if there's more images in the array, get the next - if not send to congrats screen
        if (shuffledImgArray.Length > 0)
        {
            picture.loadImage(shuffledImgArray[0]);
            picture.transform.GetComponent<Image>().preserveAspect = true;
            float width = picture.transform.GetComponent<Image>().sprite.rect.width;
        } else
        {
            SceneManager.LoadScene("Congratulations");
        }
         
    }

    public void removeImage()
    {
        // image being used is always at index 0
        List<Sprite> tempList = new List<Sprite>(shuffledImgArray);
        tempList.RemoveAt(0);
        shuffledImgArray = tempList.ToArray();
    }

    /* this is used by ItemSlot to check if correct answer 
     * maybe better way to do this but for now leaving here as GameManager attached to player <-------------
     */
    public bool isMatch(string img, string slot)
    {
        // first 3 chars of img are 'pub' or 'prv' always - check against name of slot
        if (img.Substring(0, 3) == slot)
        {
            // play congrats sound and activate the tick
            sound = Resources.Load<AudioClip>("Audio/yeah");
            audio_source.clip = sound;
            audio_source.Play();
            tick.gameObject.SetActive(true);
            return true;
        } else
        {
            // play bah bow - wrong
            sound = Resources.Load<AudioClip>("Audio/bahbow");
            audio_source.clip = sound;
            audio_source.Play();
            return false;
        }
    }

    private Sprite[] shuffle(Sprite[] imgArray)
    {
        // takes an array of sprites and shuffles into a new array
        // found beter way to do this https://stackoverflow.com/a/108836 will do a test before next s4k proto
        Sprite[] shuffledArray = new Sprite[imgArray.Length];
        int rndNo;

        System.Random rnd = new System.Random();
        for (int i = imgArray.Length; i >= 1; i--)
        {
            rndNo = rnd.Next(1, i + 1) - 1;
            shuffledArray[i - 1] = imgArray[rndNo];
            imgArray[rndNo] = imgArray[i - 1];
        }
        return shuffledArray;
    }

    public void nextRound(bool match, GameObject image)
    {
        if (match)
        {
            // play congrats
            // wait for secs
            StartCoroutine(waitABit(match));
            

        } else
        {
            // play bah bow
            // wait for secs
            StartCoroutine(waitABit(match));
            
        }

    }

    private IEnumerator waitABit(bool match)
    {
        if (match)
        {
            yield return new WaitForSeconds(3.0f);
            // delete pic from list
            removeImage();
            matchCount = matchCount - 1;
            tick.gameObject.SetActive(false);
            // load the next image 
            getNext();
            // reset currentParent of picture
            picture.GetComponent<Transform>().SetParent(startSlot.GetComponent<Transform>());
            // reset the position of the picture
            picture.GetComponent<RectTransform>().position = startSlot.GetComponent<RectTransform>().position;
        } else
        {
            yield return new WaitForSeconds(3.0f);
            // put the image to the back of the queue
            putToBack();
            // load next image 
            getNext();
            // reset currentParent of picture
            picture.GetComponent<Transform>().SetParent(startSlot.GetComponent<Transform>());
            // reset the position of the picture
            picture.GetComponent<RectTransform>().position = startSlot.GetComponent<RectTransform>().position;
        }
        
    }

    public void putToBack()
    {
        // when kid chooses wrong answer send the pic to the back of teh array so it comes up again
        Sprite currentImg = shuffledImgArray[0];
        
        for (int i = 0; i < (shuffledImgArray.Length - 1); i++)
        {
            shuffledImgArray[i] = shuffledImgArray[i + 1];
        }
        shuffledImgArray[shuffledImgArray.Length - 1] = currentImg;
    }
}

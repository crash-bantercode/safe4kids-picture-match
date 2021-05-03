using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{    
    AudioSource audio_source;
    AudioClip sound;

    public void onMouseDown() {
        audio_source = gameObject.AddComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Audio/button");
        audio_source.clip = sound;
        audio_source.Play();
        SceneManager.LoadScene("CoverPage");
    }
}

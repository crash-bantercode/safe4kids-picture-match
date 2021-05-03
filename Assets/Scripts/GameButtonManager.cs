using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonManager : MonoBehaviour
{
    public Button startButton;
    public Button backButton;
    public GameObject startPanel;

    AudioSource audio_source;
    AudioClip sound;

    private void Awake()
    {
        startPanel.SetActive(true);
        if (startButton != null)
        startButton.onClick.AddListener(startGame);
        backButton.onClick.AddListener(goBack);

        audio_source = gameObject.AddComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Audio/button");
    }

    public void startGame()
    {
        audio_source.clip = sound;
        audio_source.Play();
        startPanel.SetActive(false);
        
    }

    public void goBack()
    {
        audio_source.clip = sound;
        audio_source.Play();
        SceneManager.LoadScene("CoverPage");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

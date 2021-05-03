using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button EasyButton;
    public Button HardButton;
    public string easyGameSceneName;
    public string hardGameSceneName;
    AudioSource audio_source;
    AudioClip sound;
    public Settings settings;
    
    public void Awake() {
        EasyButton.onClick.AddListener(EasyGame);
        HardButton.onClick.AddListener(HardGame);
        audio_source = gameObject.AddComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Audio/button");
    }

    public void EasyGame() {
        audio_source.clip = sound;
        audio_source.Play();
        settings.setDifficulty("easy");
        SceneManager.LoadScene("Game");
    }

    public void HardGame() {
        audio_source.clip = sound;
        audio_source.Play();
        settings.setDifficulty("hard");
        SceneManager.LoadScene("Game");
    }
}

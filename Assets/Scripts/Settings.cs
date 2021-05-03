using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static string difficulty;

    private void Awake()
    {
        // this will persist through scenes
        DontDestroyOnLoad(transform.gameObject);
    }

    public void setDifficulty(string dif)
    {
        difficulty = dif;
    }

    public string getDifficulty()
    {
        return difficulty;
    }
}

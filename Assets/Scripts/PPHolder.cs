using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPHolder : MonoBehaviour
{

    public static PPHolder instance;

    private void Awake()
    {
        instance = this;// to always be active
        if (!PlayerPrefs.HasKey("Highscore")) { PlayerPrefs.SetInt("Highscore", 0); }
        if (!PlayerPrefs.HasKey("LastScore")) { PlayerPrefs.SetInt("LastScore", 0); }
    }


    public void TrySettingNewScore(int NewScore) 
    {
        if (GetHighscore() < NewScore) 
        {
            PlayerPrefs.SetInt("Highscore", NewScore);
        }
        PlayerPrefs.SetInt("LastScore", NewScore);
    }

    public int GetHighscore() 
    {
        return PlayerPrefs.GetInt("Highscore");
    }
    public int GetLastScore()
    {
        return PlayerPrefs.GetInt("LastScore");
    }

}

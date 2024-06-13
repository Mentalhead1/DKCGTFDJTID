using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPHolder : MonoBehaviour
{

    public static PPHolder instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;// to always be active
        }
        if (!PlayerPrefs.HasKey("Highscore")) { PlayerPrefs.SetInt("Highscore", 0); }
        if (!PlayerPrefs.HasKey("LastScore")) { PlayerPrefs.SetInt("LastScore", 0); }
        
        if (!PlayerPrefs.HasKey("Muted")) { PlayerPrefs.SetInt("Muted", 0); }
    }


    public void TrySettingNewScore(int NewScore) 
    {
        if (GetHighscore() < NewScore) 
        {
            FloatingText FT = UIController.instance.SpawnFloatingText("New Highscore!", new Vector3(Screen.width/2f,Screen.height/2f,0f));
            FT.UpdateTextFontSize(Mathf.RoundToInt(FT.text.fontSize + 30));

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    
    public Button PlayButton;
    public Button MuteButton;

    public TextMeshProUGUI Highscore, LastScore, CurrentLevel, Matches, Turns, Score;

    public GameObject MenuTexts, GameTexts;

    public GameObject FloatingTextPrefab;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameController.instance.OnGameStateChange += UpdateGameScores;
        UpdateMenuScores();
        GameTexts.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);

        HandleMuteUnmute();
    }

    private void OnDestroy()
    {
        if (GameController.instance != null)
        {
            GameController.instance.OnGameStateChange -= UpdateGameScores;
        }
    }

    public void StartGame() 
    {
        if (AudioController.Instance != null) { AudioController.Instance.PlaySound(AudioController.Instance.ButtonClick); }

        MenuTexts.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);

        GameController.instance.InitializeNewGame();
        GameTexts.gameObject.SetActive(true);
    }

    public void DisplayEndGame() 
    {
        UpdateMenuScores();

        GameTexts.gameObject.SetActive(false);
        MenuTexts.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
    }

    public void UpdateGameScores() 
    {
        Debug.Log("Updating Game Texts");

        CurrentLevel.text = "Level: " + GameController.instance.CurrentLevel + "/" + GameController.instance.levels.AllLevels.Count;
        Matches.text = "Matches: " + GameController.instance.CorrectMatches;
        Turns.text = "Turns: " + GameController.instance.CurrentTurns;
        Score.text = "Score: " + GameController.instance.CurrentScore;
    }

    public void UpdateMenuScores() 
    {
        Highscore.text = "Highscore: " + PPHolder.instance.GetHighscore();
        LastScore.text = "Last Score: " + PPHolder.instance.GetLastScore();
    }

    public FloatingText SpawnFloatingText(string Text, Vector3 Position)
    {
        GameObject tempFloatingTextObj = Instantiate(FloatingTextPrefab);
        FloatingText tempFloatingText = tempFloatingTextObj.GetComponent<FloatingText>();
        tempFloatingText.UpdateText(Text);
        tempFloatingTextObj.transform.SetParent(this.transform);
        tempFloatingTextObj.transform.position = Position;
        return tempFloatingText;
    }

    public void ResetScores() 
    {
        if (AudioController.Instance != null) { AudioController.Instance.PlaySound(AudioController.Instance.ButtonClick); }

        PlayerPrefs.DeleteKey("Highscore");
        PlayerPrefs.DeleteKey("LastScore");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void HandleMuteUnmute() 
    {
        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            MuteButton.GetComponent<Image>().color = Color.white;
            AudioController.Instance.MuteUnmute(false);
        }
        else 
        {
            MuteButton.GetComponent<Image>().color = Color.grey;
            AudioController.Instance.MuteUnmute(true);
        }
    }

    public void MuteUnmute() 
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            // Unmute
            PlayerPrefs.SetInt("Mute", 0);
        }
        else 
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        HandleMuteUnmute();
        Debug.Log("Mute:" + PlayerPrefs.GetInt("Mute").ToString());
    }

}

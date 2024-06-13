using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameStarter Starter;
    public Levels levels;

    private Card ActiveCard = null;
    private Card otherWrongActiveCard = null;

    private bool GameIsActive = false;
    private float GameStartTime = 2f;
    private float GameTime = 0;
    private bool GameControlsStarted = false;

    public int CurrentLevel = 0;
    public int CorrectMatches = 0;
    public int CurrentScore = 0;
    public int CurrentTurns = 0;

    private int ComboCounter = 0;

    public event Action OnGameControlsStarted;
    private void DoGameControlsStarted() { OnGameControlsStarted?.Invoke(); }
    
    public event Action OnGameStateChange;
    private void DoGameStateChange() { OnGameStateChange?.Invoke(); }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //StartLevel(CurrentLevel);
    }

    public void InitializeNewGame() 
    {
        CurrentLevel = 0;
        CurrentTurns = 0;
        CurrentScore = 0;
        CorrectMatches = 0;
        GameIsActive = true;

        StartLevel(CurrentLevel);
    }

    public void StartLevel(int currentLevel) 
    {
        ClearForStart();
        Starter.StartGame(levels.AllLevels[currentLevel]);
    }

    public void StartRandomGame()
    {
        ClearForStart();
        Starter.StartGame(levels.GetRandomLevel());
    }

    private void ClearForStart() 
    {
        GameControlsStarted = false;
        GameTime = 0;
        CorrectMatches = 0;
        CurrentTurns = 0;
        ComboCounter = 0;
    }

    public void CardClicked(Card card) 
    {
        if (!CanUseControls()) { return; }

        card.RotateCard();

        card.SetClickingAbility(false);

        // nothing clicked yet
        if (ActiveCard == null) 
        { 
            ActiveCard = card;
        }

        // two wrong cards were clicked before
        else if (otherWrongActiveCard != null) 
        {
            ActiveCard.RotateCard();
            ActiveCard.SetClickingAbility(true);
            otherWrongActiveCard.RotateCard();
            otherWrongActiveCard.SetClickingAbility(true);

            otherWrongActiveCard = null;
            ActiveCard = card;
        }

        // clicked second right card
        else if (ActiveCard.TypeID == card.TypeID)
        {
            CurrentTurns++;
            CorrectMatches++;
            CurrentScore += 1 + ComboCounter;
            ComboCounter++;

            ActiveCard.SetClickingAbility(false);
            card.SetClickingAbility(false);

            //ActiveCard.gameObject.SetActive(false);
            //card.gameObject.SetActive(false);
            ActiveCard.transform.localScale *= 0.5f;
            card.transform.localScale *= 0.5f;
            
            ActiveCard = null;

            if (IsLevelDone()) { GoToNextLevel(); }
        }

        // clicked wrong card
        else 
        {
            ComboCounter = 0;
            CurrentTurns++;
            otherWrongActiveCard = card;
        }

        DoGameStateChange();
    }

    private bool IsLevelDone() 
    {
        if (CorrectMatches >= Starter.CardsHolder.childCount / 2) 
        {
            return true;
        }

        return false;
    }

    public void GoToNextLevel() // public only for Testing purposes
    {
        CurrentLevel++;
        if (CurrentLevel >= levels.AllLevels.Count) 
        {
            // Display Menu
            EndGame();
            return;
        }
        StartLevel(CurrentLevel);
    }

    public bool CanUseControls()
    {
        if (GameTime > GameStartTime) 
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;

        if (!GameControlsStarted && GameTime >= GameStartTime) 
        {
            GameControlsStarted = true;
            DoGameControlsStarted();
        }
    }

    private void EndGame() 
    {
        GameIsActive = false;
        CurrentLevel = 0;
        PPHolder.instance.TrySettingNewScore(CurrentScore);
        UIController.instance.DisplayEndGame();
    }
}

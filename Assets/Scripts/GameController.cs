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

    private float GameStartTime = 2f;
    private float GameTime = 0;
    private bool GameControlsStarted = false;
    private int CurrentLevel = 0;
    private int CorrectMatches = 0;

    public event Action OnGameControlsStarted;
    private void DoGameControlsStarted() { OnGameControlsStarted?.Invoke(); }

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
        StartLevel(CurrentLevel);
    }

    private void StartLevel(int currentLevel) 
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
            ActiveCard.SetClickingAbility(false);
            card.SetClickingAbility(false);

            //ActiveCard.gameObject.SetActive(false);
            //card.gameObject.SetActive(false);
            ActiveCard.transform.localScale *= 0.5f;
            card.transform.localScale *= 0.5f;
            
            ActiveCard = null;

            CorrectMatches++;
            if (IsLevelDone()) { GoToNextLevel(); }
        }

        // clicked wrong card
        else 
        {
            otherWrongActiveCard = card;
        }

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
}

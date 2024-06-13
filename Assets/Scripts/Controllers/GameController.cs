using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameStarter Starter;
    public Levels levels;
    private int TotalCards;

    private Card ActiveCard = null;
    private Card otherWrongActiveCard = null;

    private float GameStartTime = 2f;
    private float GameTime = 0;
    private bool GameControlsStarted = false;

    private float TimeAfterLastCardClick = 0;

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

        StartLevel(CurrentLevel);
    }

    public void StartLevel(int currentLevel) 
    {
        ClearForStart();
        TotalCards = levels.AllLevels[currentLevel].LayoutX * levels.AllLevels[currentLevel].LayoutY;
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

        TimeAfterLastCardClick = 0;

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
            HandleCorrect(card);

            ActiveCard.SetClickingAbility(false);
            card.SetClickingAbility(false);

            ActiveCard.ScaleDown();
            card.ScaleDown();
            
            ActiveCard = null;

            if (IsLevelDone()) { GoToNextLevel(); }
        }

        // clicked wrong card
        else 
        {
            HandleWrong();
            otherWrongActiveCard = card;
        }

        DoGameStateChange();
    }

    private void HandleCorrect(Card pressedCard) 
    {
        CurrentTurns++;
        CorrectMatches++;
        int ScoreToAdd = 1 + ComboCounter;
        CurrentScore += ScoreToAdd;

        FloatingText FT = UIController.instance.SpawnFloatingText("+" + ScoreToAdd.ToString(), Camera.main.WorldToScreenPoint(pressedCard.transform.position));
        FT.UpdateTextFontSize(Mathf.RoundToInt(FT.text.fontSize + 5 * ComboCounter));

        ComboCounter++;
    }

    private void HandleWrong() 
    {
        ComboCounter = 0;
        CurrentTurns++;
    }

    private bool IsLevelDone() 
    {
        //if (CorrectMatches >= CardsHolder.instance.transform.childCount / 2) 
        if (CorrectMatches >= TotalCards / 2) 
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
        StartCoroutine(NextLevelCoroutine(1.5f));
    }

    private IEnumerator NextLevelCoroutine(float delayTime) 
    {
        yield return new WaitForSeconds(delayTime);
        StartLevel(CurrentLevel);
        DoGameStateChange();
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
        
        TimeAfterLastCardClick += Time.deltaTime;
        if (TimeAfterLastCardClick > 3) 
        {
            if (ActiveCard != null && otherWrongActiveCard != null) 
            {

                ActiveCard.RotateCard();
                ActiveCard.SetClickingAbility(true);
                otherWrongActiveCard.RotateCard();
                otherWrongActiveCard.SetClickingAbility(true);

                otherWrongActiveCard = null;
                ActiveCard = null;

                TimeAfterLastCardClick = 0;
            }
        }

    }

    private void EndGame() 
    {
        Starter.ClearActiveCards();

        CurrentLevel = 0;
        PPHolder.instance.TrySettingNewScore(CurrentScore);
        UIController.instance.DisplayEndGame();
    }
}

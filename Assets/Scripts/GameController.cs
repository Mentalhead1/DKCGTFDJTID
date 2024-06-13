using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameStarter Starter;
    public Levels levels;

    private Card ActiveCard = null;
    private Card otherWrongActiveCard = null;

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

    public void StartRandomGame()
    {
        Starter.StartGame(levels.GetRandomLevel());
    }

    public void CardClicked(Card card) 
    {
        card.RotateCard();

        Debug.Log("Clicked Card with ID: " + card.TypeID);

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
        }

        // clicked wrong card
        else 
        {
            otherWrongActiveCard = card;
        }

    }
}

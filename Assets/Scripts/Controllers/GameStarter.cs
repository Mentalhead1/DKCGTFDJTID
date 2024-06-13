using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameController GC;
    public GameObject CardPrefab;
    public Transform CardsHolderTransform;

    public Vector2 Padding = new Vector2(0.5f,0.5f);
    public List<Color> Colors = new List<Color>();

    private List<Transform> Cards = new List<Transform>();

    private void Awake()
    {
        GC.OnGameControlsStarted += StartControlsAndFlip;
    }

    private void OnDestroy()
    {
        GC.OnGameControlsStarted -= StartControlsAndFlip;
    }

    public void StartGame(Level levelToStart) 
    {
        //Clear Cards
        ClearActiveCards();
        //Get New Random Colors. may be changed to materials later on
        InitializeRandomColors();
        //Set The Holder to it's default position
        CardsHolderTransform = CardsHolder.instance.transform;
        CardsHolderTransform.position = Vector3.zero;

        // Instantiate Cards and set them according to the layout
        int GameLayoutX = levelToStart.LayoutX;
        int GameLayoutY = levelToStart.LayoutY;

        Debug.Log("Initializing: (" + GameLayoutX + ":" + GameLayoutY + ")");

        if (!IsLayoutValid(GameLayoutX,GameLayoutY)) { return; }

        CreateCards(GameLayoutX, GameLayoutY);

        SetAllCardTypes();

        // Adjust camera to fit all cards
        Camera.main.GetComponent<CameraRepositioner>().Reposition(CardsHolderTransform.gameObject);
    }

    private void StartControlsAndFlip() 
    {
        foreach (Transform C in Cards)
        {
            if (C != null)
            {
                C.GetComponent<Card>().RotateCard();
            }
        }
    }

    private void CreateCards(int X, int Y) 
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                Transform TempCard = Instantiate(CardPrefab).transform;
                TempCard.SetParent(CardsHolderTransform);
                SetCardPosition(TempCard, i, j);
                Cards.Add(TempCard);
            }
        }

        CardsHolderTransform.localPosition -= Cards[Cards.Count - 1].transform.localPosition / 2f;
    }

    private void SetCardPosition(Transform card,int i, int j) 
    {
        Vector3 CardScale = card.transform.localScale;

        card.localPosition = new Vector3(i * CardScale.x + Padding.x * i, j * CardScale.y + Padding.y * j, 0);
    }

    private void SetAllCardTypes() 
    {
        Cards.Shuffle();

        for (int i = 0; i < Cards.Count; i++) 
        {
            Cards[i].GetComponent<Card>().SetCard(i / 2, Colors[i / 2]);
        }

        foreach (Transform C in Cards) 
        {
            C.GetComponent<Card>().RotateCard(false);
        }
    }

    private bool IsLayoutValid(int X, int Y) 
    {
        if (X <= 0 || Y <= 0)
        {
            Debug.LogError("Invalid layout");
            return false;
        }
        if (X + Y <= 1)
        {
            Debug.LogError("Invalid layout");
            return false;
        }
        if ((X * Y) % 2 == 1) // or check both by "%2". doing this once should, on average, be better
        {
            Debug.LogError("Invalid layout");
            return false;
        }

        return true;
    }

    public void ClearActiveCards() 
    {
        foreach (Transform C in Cards) 
        {
            if (C != null)
            {
                Destroy(C.gameObject);
            }
        }
        Cards.Clear();
    }

    private void InitializeRandomColors() 
    {
        Colors.Clear();
        for (int i = 0; i < 100; i++) 
        {
            Colors.Add(new Color(Random.Range(0f, 0.85f), Random.Range(0f, 0.85f), Random.Range(0f, 0.85f)));
        }
    }
}

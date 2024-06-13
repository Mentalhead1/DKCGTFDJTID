using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{

    public Camera mainCamera;
    public GameObject CardPrefab;
    public Material[] Materials;
    public Levels LevelsHolder;

    public Vector2 Padding = new Vector2(0.5f,0.5f);

    List<Transform> Cards = new List<Transform>();

    public void StartGame(Level levelToStart) 
    {
        //Clear 
        ClearActiveCards();

        // Instantiate Cards and set them according to the layout
        int GameLayoutX = levelToStart.LayoutX;
        int GameLayoutY = levelToStart.LayoutY;
        if (GameLayoutX <= 0 || GameLayoutY <= 0) 
        {
            Debug.LogError("Invalid layout");
            return;
        }
        if (GameLayoutX + GameLayoutY <= 1) 
        {
            Debug.LogError("Invalid layout");
            return;
        }
        if ((GameLayoutX * GameLayoutY)%2 == 1) // or check both by "%2". doing this once should, on average, be better
        {
            Debug.LogError("Invalid layout");
            return;
        }


        for (int i = 0; i < GameLayoutX;i++) 
        {
            for (int j = 0; j < GameLayoutY; j++) 
            {
                Transform TempCard = Instantiate(CardPrefab).transform;
                Vector3 CardScale = TempCard.transform.localScale;
                TempCard.localPosition = new Vector3(j * CardScale.x + Padding.x * j, i * CardScale.y + Padding.y * i, 0) - new Vector3();
                Cards.Add(TempCard);
            }
        }




        // Adjust camera to fit all cards
    }

    public void ClearActiveCards() 
    {
        foreach (Transform C in Cards) 
        {
            Destroy(C.gameObject);
        }
        Cards.Clear();
    }

}

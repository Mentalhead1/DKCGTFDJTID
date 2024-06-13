using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TestingController : MonoBehaviour
{

    public GameController GC;
    public GameStarter GS;

    void Start()
    {
#if !UNITY_EDITOR
    Destroy(this);
#endif

        GC = GetComponent<GameController>();
        GS = GetComponent<GameStarter>();
    }

    void Update()
    {
        // Restart Scene
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Debug.LogError("Trying to restart game");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        }
        
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            Debug.LogError("Pressed N to go to Random level");
            GC.StartRandomGame();
        }
        
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            Debug.LogError("Pressed Y to go to next level");
            GC.GoToNextLevel();
        }
        
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            Debug.LogError("Deleting All playerprefs");
            PlayerPrefs.DeleteAll();
        }
    }

}

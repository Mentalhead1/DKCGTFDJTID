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
    Destroy(this.gameObject);
#endif

        GC = GetComponent<GameController>();
        GS = GetComponent<GameStarter>();
    }

    void Update()
    {
        // Restart Scene
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        }
        
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            GC.StartRandomGame();
        }
    }

}

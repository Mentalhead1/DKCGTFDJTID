using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperObjectsHolder : MonoBehaviour
{
    public static HelperObjectsHolder instance;

    public Transform Flipped, Unflipped;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsHolder : MonoBehaviour
{
    public static CardsHolder instance;

    private void Awake()
    {
        instance = this;
    }
}

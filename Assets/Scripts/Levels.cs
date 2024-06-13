using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public List<Level> AllLevels;

    private void Awake()
    {
        InitializeLevels();
    }

    private void InitializeLevels() 
    {
        AllLevels.Add(new Level(2, 3));//6
        AllLevels.Add(new Level(4, 2));//8
        AllLevels.Add(new Level(3, 4));//12
        AllLevels.Add(new Level(2, 6));//12
        AllLevels.Add(new Level(4, 3));//12
        AllLevels.Add(new Level(4, 4));//16
        AllLevels.Add(new Level(3, 6));//18
        AllLevels.Add(new Level(3, 8));//24
        AllLevels.Add(new Level(5, 6));//30
    }

    public Level GetRandomLevel() 
    {
        int RandomForXorYEven = Random.Range(0, 2);// should be valid, on average more Y than X. and in total, more tan 2
        if (RandomForXorYEven == 0)
        {
            return new Level(Random.Range(1, 4) * 2, Random.Range(2, 5));
        }
        else 
        { 
            return new Level(Random.Range(2, 8), Random.Range(1, 3) * 2);
        }
    }
}

[System.Serializable]
public class Level 
{
    public int LayoutX;
    public int LayoutY;

    public Level(int _X, int _Y) 
    {
        LayoutX = _X;
        LayoutY = _Y;
    }

}

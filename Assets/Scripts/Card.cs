using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public MeshRenderer CardFaceRenderer;
    public int TypeID;

    public void SetCard(int _TypeID, Material _Material) 
    {
        TypeID = _TypeID;
        CardFaceRenderer.material = _Material;
    }
    
    public void SetCard(int _TypeID, Color _MaterialColor) 
    {
        TypeID = _TypeID;
        CardFaceRenderer.material.color = _MaterialColor;
    }
}

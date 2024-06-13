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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

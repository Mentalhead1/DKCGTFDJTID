using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class Card : MonoBehaviour
{
    public MeshRenderer CardFaceRenderer;
    public int TypeID;
    public TextMeshPro IDTextDisplay;

    private bool ShouldBeAbleToClickOnThisCard = true;
    public void SetCard(int _TypeID, Material _Material) 
    {
        SetCard(_TypeID, _Material.color);
    }

    public void SetCard(int _TypeID, Color _MaterialColor) 
    {
        TypeID = _TypeID;
        CardFaceRenderer.material.color = _MaterialColor;
        IDTextDisplay.SetText(_TypeID.ToString());

        ShouldBeAbleToClickOnThisCard = true;
    }

    private void OnCardClick() 
    {
        GameController.instance.CardClicked(this);
    }

    public void RotateCard() 
    {
        transform.Rotate(new Vector3(180f, 0, 0));
    }

    private void OnMouseDown()
    {
        if (ShouldBeAbleToClickOnThisCard)
        {
            OnCardClick();
        }
    }

    public void SetClickingAbility(bool Clickable) 
    {
        ShouldBeAbleToClickOnThisCard = Clickable;
    }
}

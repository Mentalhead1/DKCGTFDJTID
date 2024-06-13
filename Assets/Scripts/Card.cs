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

    //Animations
    private bool IsFacingDown = true;
    private bool ShouldBeAnimating = false;
    private Vector3 TargetPosition;
    private Transform TargetRotation;
    private Vector3 OriginalPosition;
    public float AnimationLerpTime = 5f;

    private bool ShouldBeAnimatingScale = false;
    private Vector3 TargetScale;

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

        OriginalPosition = transform.localPosition;
    }

    private void OnCardClick() 
    {
        GameController.instance.CardClicked(this);
    }

    public void RotateCard(bool WithAnimation = true) 
    {
        Debug.Log("Rotating");
        IsFacingDown = !IsFacingDown;
        if (!WithAnimation)
        {
            ShouldBeAnimating = false;
            transform.Rotate(new Vector3(180f, 0, 0));
        }
        else 
        {
            ShouldBeAnimating = true;
            if (IsFacingDown)
            {
                TargetPosition = OriginalPosition + new Vector3(0f, 0.3f, 0.2f);
                TargetRotation = HelperObjectsHolder.instance.Flipped.transform;
            }
            else 
            {
                TargetPosition = OriginalPosition;
                TargetRotation = HelperObjectsHolder.instance.Unflipped.transform;
            }
        }
    }

    public void ScaleDown() 
    {
        ShouldBeAnimatingScale = true;
        TargetScale = transform.localScale * 0.5f;
    }

    private void Update()
    {
        if (ShouldBeAnimating) 
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, AnimationLerpTime * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, TargetRotation.rotation, AnimationLerpTime * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, TargetPosition) < 0.01f) 
            {
                ShouldBeAnimating = false;
                transform.localPosition = TargetPosition;
                transform.localRotation = TargetRotation.rotation;
            }
        }

        if (ShouldBeAnimatingScale) 
        {
            transform.localScale = Vector3.Lerp(transform.localScale, TargetScale,AnimationLerpTime * Time.deltaTime);
            if (Vector3.Distance(transform.localScale, TargetScale) < 0.01f) 
            {
                transform.localScale = TargetScale;
                ShouldBeAnimatingScale = false;
            }
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float FloatUpSpeed = 3f;
    public float FloatSideSpeed = 1f;
    public float DisappearSpeed = 0.01f;
    public float DisappearAfterTime = 1f;
    public bool HasGravity = false;
    public float Gravity = 0f;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
        FloatSideSpeed = 0.75f;
        FloatSideSpeed = Random.Range(-FloatSideSpeed, FloatSideSpeed);

        DisappearAfterTime = 1.5f;
        DisappearSpeed = 0.014f;
        FloatUpSpeed = 25f;
    }

    private void Start()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-0.05f, 0.05f), transform.position.y + Random.Range(-0.05f, 0.05f), transform.position.z + 20f);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + FloatSideSpeed * Time.deltaTime, transform.position.y + FloatUpSpeed * Time.deltaTime, transform.position.z);

        DisappearAfterTime -= Time.deltaTime;
        if (DisappearAfterTime > 0f) return;

        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - DisappearSpeed);
        if (text.color.a < 0.01f)
        {
            Destroy(gameObject);
        }

        if (HasGravity)
        {
            FloatUpSpeed -= Gravity * Time.deltaTime;
        }

    }

    public void UpdateText(string Text)
    {
        text.text = Text;
    }
    
    public void UpdateTextFontSize(int FontSize)
    {
        text.fontSize = FontSize;
    }
    public void UpdateColor(Color Col)
    {
        text.color = Col;
    }
    public void UpdateSpeed(Vector2 Direction)
    {
        FloatSideSpeed = Direction.x;
        FloatUpSpeed = Direction.y;
    }

    public void UpdateGravity(float GravityModifier)
    {
        HasGravity = true;
        Gravity = GravityModifier;
    }

    public void UpdateDisappearAfterTime(float NewDisappearAfterTime)
    {
        DisappearAfterTime = NewDisappearAfterTime;
    }

    public void UpdateDisappearSpeed(float NewDisappearSpeed = 0.014f)
    {
        DisappearSpeed = NewDisappearSpeed;
    }

}

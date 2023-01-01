using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBox : MonoBehaviour
{
    public ColorCode colorCode;
    public ColorTheme colorTheme;
    [Range(0, 2)]
    public int colorGrade;

    [HideInInspector]
    public bool isInBox = false;

    private SpriteRenderer myRenderer;
    private Color myColor;
    private string layerName = "Default";

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        applyColor();
        applyLayer();
    }

    public void applyColor()
    {
        if(myRenderer == null)
        {
            myRenderer = GetComponent<SpriteRenderer>();
        }

        switch (colorTheme)
        {
            case ColorTheme.Blue:
                myColor = colorCode.BlueColorTheme[colorGrade];
                layerName = "Blue" + colorGrade.ToString();
                break;
            case ColorTheme.Red:
                myColor = colorCode.RedColorTheme[colorGrade];
                layerName = "Red" + colorGrade.ToString();
                break;
        }

        myRenderer.color = myColor;
    }

    public void applyLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    private void OnValidate()
    {
        applyColor();
        applyLayer();
    }

}

public enum ColorTheme
{
    Blue,
    Red
}

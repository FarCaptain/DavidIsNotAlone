using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public ColorCode colorCode;
    public ColorTheme colorTheme;
    [Range(0, 2)]
    public int colorGrade;

    public SpriteRenderer myRenderer;
    public Animator myAnimator;

    private Color myColor;
    private string layerName = "Default";

    private void Start()
    {
        // init
        applyColor();
        applyLayer();
    }

    public void applyColor()
    {
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

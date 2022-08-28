using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    public ColorCode colorCode;
    public ColorTheme colorTheme;
    [Range(0, 2)]
    public int colorGrade;

    public SpriteRenderer myRenderer;

    private bool isInBox = false;

    private Color myColor;
    private string layerName = "Default";

    private void Start()
    {
        // init
        applyColor();
    }

    public bool IsInBox()
    {
        return isInBox;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject;
        if(player.tag == "Player")
        {
            if(player.layer == LayerMask.NameToLayer(layerName))
            {
                //Win
                Debug.Log("Win!");
                isInBox = true;
            }
            else
            {
                //Warn
                AudioManager.instance.Play("Wrong");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject;
        if (player.tag == "Player")
        {
            if (player.layer == LayerMask.NameToLayer(layerName))
            {
                // left the box
                isInBox = false;
            }
        }
    }

    private void OnValidate()
    {
        applyColor();
    }
}

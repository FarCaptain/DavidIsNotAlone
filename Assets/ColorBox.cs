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
    private Collider2D myCollider;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        string layerName = "Default";
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
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject player = collision.gameObject;
    //    if (player.tag == "Player")
    //    {
    //        var playerColor = player.GetComponent<PlayerProperty>();
    //        if(playerColor.colorTheme == colorTheme && playerColor.colorGrade == colorGrade )
    //        {
    //            // suck player in
    //            isInBox = true;
    //            //gameObject.layer = LayerMask.NameToLayer("RedColorBox");
    //            playerColor.colorGrade++;
    //            playerColor.applyColor();
    //        }
    //        else
    //        {
    //            gameObject.layer = LayerMask.NameToLayer("Default");
    //        }
    //    }
    //}
}

public enum ColorTheme
{
    Blue,
    Red
}

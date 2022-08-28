using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerColorBox : MonoBehaviour
{
    public ColorBox colorBox;
    private bool isInBox = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isInBox)
        {
            GameObject player = collision.gameObject;
            var playerColor = player.GetComponent<PlayerProperty>();

            // throw player out
            playerColor.myAnimator.SetTrigger("Jump");
            isInBox = false;
            playerColor.applyLayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInBox)
            return;

        GameObject player = collision.gameObject;
        if (player.tag == "Player")
        {
            var playerColor = player.GetComponent<PlayerProperty>();
            if (playerColor.colorTheme == colorBox.colorTheme && playerColor.colorGrade == colorBox.colorGrade)
            {
                // suck player in
                isInBox = true;

                playerColor.myAnimator.SetTrigger("Jump");
                playerColor.colorGrade++;
                playerColor.applyColor();
            }
        }
    }
}

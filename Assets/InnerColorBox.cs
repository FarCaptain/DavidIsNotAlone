using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerColorBox : MonoBehaviour
{
    public ColorBox colorBox;
    private bool isInBox = false;
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = Camera.main.transform.GetComponent<CameraShake>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isInBox)
        {
            GameObject player = collision.gameObject;
            var playerColor = player.GetComponent<PlayerProperty>();

            // throw player out
            playerColor.myAnimator.SetTrigger("Jump");
            StartCoroutine(cameraShake.Shake(.15f, .2f));

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
                StartCoroutine(cameraShake.Shake(.15f, .2f));
                playerColor.colorGrade++;
                playerColor.applyColor();
            }
        }
    }
}

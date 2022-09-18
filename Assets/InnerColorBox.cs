using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerColorBox : MonoBehaviour
{
    public ColorBox colorBox;
    private GameObject objectInBox;
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = Camera.main.transform.GetComponent<CameraShake>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (objectInBox && objectInBox == player)
        {
            var playerColor = player.GetComponent<PlayerProperty>();

            // throw player out
            playerColor.myAnimator.SetTrigger("Pop");
            AudioManager.instance.Play("Pop");
            StartCoroutine(cameraShake.Shake(.15f, .2f));

            objectInBox = null;
            playerColor.applyLayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectInBox)
            return;

        GameObject player = collision.gameObject;
        if (player.tag == "Player")
        {
            var playerColor = player.GetComponent<PlayerProperty>();
            if (playerColor.colorTheme == colorBox.colorTheme && playerColor.colorGrade == colorBox.colorGrade)
            {
                // suck player in
                objectInBox = player;

                playerColor.myAnimator.SetTrigger("Pop");
                AudioManager.instance.Play("Pop");
                StartCoroutine(cameraShake.Shake(.15f, .2f));
                playerColor.colorGrade++;
                playerColor.applyColor();
            }
        }
    }
}

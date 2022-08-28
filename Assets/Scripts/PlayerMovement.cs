using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public bool enableControl;
    public Rigidbody2D myRigidbody;
    public GameObject indicator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool twoPlayer = false;

    private void Start()
    {
        myRigidbody.constraints = enableControl ? RigidbodyConstraints2D.FreezeRotation : 
            (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);

        indicator.SetActive(enableControl);

        var david = GameObject.Find("David");
        var steve = GameObject.Find("Steve Variant");
        if(david && steve)
        {
            twoPlayer = true;
        }
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            AudioManager.instance.Play("Jump");
        }

        if (twoPlayer && Input.GetButtonDown("SwitchPlayer"))
        {
            enableControl = !enableControl;
            myRigidbody.constraints = enableControl ? RigidbodyConstraints2D.FreezeRotation :
                (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);

            indicator.SetActive(enableControl);
        }
    }

    private void FixedUpdate()
    {
        if(enableControl)
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

        if(transform.position.y < -15.0f)
        {
            // restart game
            AudioManager.instance.Play("Die");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

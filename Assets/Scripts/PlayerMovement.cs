using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public bool enableControl;
    public Rigidbody2D myRigidbody;

    private float horizontalMove = 0f;
    private bool jump = false;

    private void Start()
    {
        myRigidbody.constraints = enableControl ? RigidbodyConstraints2D.FreezeRotation : 
            (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if(Input.GetButtonDown("SwitchPlayer"))
        {
            enableControl = !enableControl;
            myRigidbody.constraints = enableControl ? RigidbodyConstraints2D.FreezeRotation :
                (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
        }
    }

    private void FixedUpdate()
    {
        if(enableControl)
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}

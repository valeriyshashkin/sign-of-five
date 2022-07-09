using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Vector3 playerVelocity;
    float playerSpeed = 7;
    float playerRunSpeed = 12;
    float jumpHeight = 1;
    float gravityValue = -9.81f * 1.5f;
    bool jump;
    bool run;
    float holdingShiftTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire3"))
        {
            if (holdingShiftTime >= 0.3)
            {
                run = false;
                holdingShiftTime = 0;
            }
        }

        if (run)
        {
            holdingShiftTime += Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire3") && !jump)
        {
            run = true;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            run = false;
        }

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);
        controller.Move(move * Time.deltaTime * (run && Input.GetAxis("Vertical") > 0 ? playerRunSpeed : playerSpeed));

        if (jump)
        {
            jump = false;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

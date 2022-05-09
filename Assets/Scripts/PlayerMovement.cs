using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    DialogueRunner dialogueRunner;
    Animator iconAnimator;
    Vector3 playerVelocity;
    float playerSpeed = 5.0f;
    float jumpHeight = 1.0f;
    float gravityValue = -9.81f;
    bool jump;
    bool makeSomeAction;
    bool inTrigger;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Interactable")
        {
            iconAnimator = collider.gameObject.transform
                .GetChild(0)
                .gameObject.GetComponent<Animator>();
            inTrigger = true;
            iconAnimator.Play("FadeIn");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Interactable")
        {
            iconAnimator = collider.gameObject.transform
                .GetChild(0)
                .gameObject.GetComponent<Animator>();
            inTrigger = false;
            iconAnimator.Play("FadeOut");
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (makeSomeAction)
        {
            iconAnimator.Play("FadeOut");
            makeSomeAction = false;
            collider.gameObject.GetComponent<YarnInteractable>().Interact();
        }
    }

    void OnDialogueComplete()
    {
        if (inTrigger)
        {
            iconAnimator.Play("FadeIn");
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    void Update()
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            return;
        }

        if (Input.GetButtonUp("Submit") && !makeSomeAction)
        {
            makeSomeAction = true;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (!dialogueRunner.IsDialogueRunning)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (jump)
            {
                jump = false;
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

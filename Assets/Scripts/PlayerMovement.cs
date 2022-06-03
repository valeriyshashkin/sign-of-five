using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    DialogueRunner dialogueRunner;
    Tip tip;
    Vector3 playerVelocity;
    float playerSpeed = 7.0f;
    float jumpHeight = 1.0f;
    float gravityValue = -9.81f;
    bool jump;

    void OnTriggerEnter(Collider collider)
    {
        var tagsComponent = collider.gameObject.GetComponent<Tags>();

        if (tagsComponent == null)
        {
            return;
        }

        if (tagsComponent.tags.Contains("Interactable") && !GameObject.ReferenceEquals(this.gameObject, collider.gameObject))
        {
            tip.Show();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        var tagsComponent = collider.gameObject.GetComponent<Tags>();

        if (tagsComponent == null)
        {
            return;
        }

        if (tagsComponent.tags.Contains("Interactable") && !GameObject.ReferenceEquals(this.gameObject, collider.gameObject))
        {
            tip.Hide();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            return;
        }

        var tagsComponent = collider.gameObject.GetComponent<Tags>();

        if (tagsComponent == null)
        {
            return;
        }

        if (tagsComponent.tags.Contains("Interactable") && !GameObject.ReferenceEquals(this.gameObject, collider.gameObject))
        {
            tip.Show();
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        tip = FindObjectOfType<Tip>();
    }

    void Update()
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            tip.Hide();
            return;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    DialogueRunner dialogueRunner;
    SpriteRenderer icon;
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
            icon = collider.gameObject.transform
                .GetChild(0)
                .gameObject.GetComponent<SpriteRenderer>();
            inTrigger = true;
            StartCoroutine(FadeInIcon());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Interactable")
        {
            icon = collider.gameObject.transform
                .GetChild(0)
                .gameObject.GetComponent<SpriteRenderer>();
            inTrigger = false;
            StartCoroutine(FadeOutIcon());
        }
    }

    IEnumerator FadeInIcon()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            icon.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator FadeOutIcon()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            icon.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (makeSomeAction)
        {
            icon = collider.gameObject.transform
                .GetChild(0)
                .gameObject.GetComponent<SpriteRenderer>();
            StartCoroutine(FadeOutIcon());
            makeSomeAction = false;
            collider.gameObject.GetComponent<YarnInteractable>().Interact();
        }
    }

    void OnDialogueComplete()
    {
        if (inTrigger)
        {
            StartCoroutine(FadeInIcon());
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

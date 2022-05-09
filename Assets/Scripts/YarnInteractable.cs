using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    Animator animator;
    public string node;
    bool userRequestAction;
    bool inTrigger;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            inTrigger = true;
            animator.Play("FadeIn");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            inTrigger = false;
            animator.Play("FadeOut");
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (userRequestAction)
        {
            userRequestAction = false;
            animator.Play("FadeOut");
            Interact();
        }
    }

    void OnDialogueComplete()
    {
        if (inTrigger)
        {
            animator.Play("FadeIn");
        }
    }

    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    void Update()
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            return;
        }

        if (Input.GetButtonUp("Submit") && inTrigger)
        {
            userRequestAction = true;
        }
    }

    void Interact()
    {
        dialogueRunner.StartDialogue(node);
    }
}

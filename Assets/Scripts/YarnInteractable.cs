using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    public string node;
    bool userRequestAction;
    bool inTrigger;

    void OnTriggerEnter(Collider collider)
    {
        var tagsComponent = collider.gameObject.GetComponent<Tags>();

        if (tagsComponent == null)
        {
            return;
        }

        if (tagsComponent.tags.Contains("Player"))
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        var tagsComponent = collider.gameObject.GetComponent<Tags>();

        if (tagsComponent == null)
        {
            return;
        }

        if (tagsComponent.tags.Contains("Player"))
        {
            inTrigger = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (userRequestAction)
        {
            userRequestAction = false;
            dialogueRunner.StartDialogue(node);
        }
    }

    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
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
}

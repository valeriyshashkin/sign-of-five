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
    bool readyForAction = false;
    Coroutine waitForActionCoroutine;

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
            waitForActionCoroutine = StartCoroutine(WaitForAction());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("Stop");

        var tagsComponent = collider.gameObject.GetComponent<Tags>();

        if (tagsComponent == null)
        {
            return;
        }

        if (tagsComponent.tags.Contains("Player"))
        {
            inTrigger = false;
            StopCoroutine(waitForActionCoroutine);
        }
    }

    IEnumerator WaitForAction()
    {
        yield return new WaitForSeconds(1);
        readyForAction = true;
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

        if (readyForAction && inTrigger)
        {
            readyForAction = false;
            userRequestAction = true;
        }
    }
}

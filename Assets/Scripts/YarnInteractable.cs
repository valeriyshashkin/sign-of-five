using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    public string node;

    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    public void Interact()
    {
        dialogueRunner.StartDialogue(node);
    }
}

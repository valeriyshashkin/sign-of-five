using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CursorController : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    public GameObject cursor;

    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 newCursorPosition = Input.mousePosition;
        newCursorPosition.z = 1;
        cursor.transform.position = Camera.main.ScreenToWorldPoint(newCursorPosition);
        cursor.SetActive(dialogueRunner.IsDialogueRunning);
    }
}

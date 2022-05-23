using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GlobalCommands : MonoBehaviour
{
    SaveController saveController;
    void Start()
    {
        FindObjectOfType<Yarn.Unity.DialogueRunner>().AddCommandHandler("quit", () => Quit());
        saveController = FindObjectOfType<SaveController>();
    }

    void Quit()
    {
        saveController.Save();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

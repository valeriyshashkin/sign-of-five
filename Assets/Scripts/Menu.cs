using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    List<GameObject> selects = new List<GameObject>();
    int selectedItem = 0;
    public bool isReturnPressed { get; set; } = false;
    void Start()
    {
        foreach (Transform child in transform)
        {
            selects.Add(child.GetChild(0).gameObject);
        }
    }

    void OnEnable()
    {
        isReturnPressed = false;
    }

    public string GetSelectedItem()
    {
        return selects[selectedItem].transform.parent.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GetSelectedItem() == "Exit")
            {
                Application.Quit();
            }
            isReturnPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedItem--;
            isReturnPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedItem++;
            isReturnPressed = false;
        }

        if (selectedItem < 0)
        {
            selectedItem = transform.childCount - 1;
        }

        if (selectedItem > transform.childCount - 1)
        {
            selectedItem = 0;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            selects[i].SetActive(i == selectedItem);
        }
    }
}

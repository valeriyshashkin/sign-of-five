using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public void Show()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}

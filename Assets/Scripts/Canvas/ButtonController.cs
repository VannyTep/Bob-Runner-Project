using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    int level = 0;

    [SerializeField] private GameObject[] Buttons;

    void Awake()
    {
        Buttons = GameObject.FindGameObjectsWithTag("lv-btn");

        foreach (GameObject btn in Buttons)
        {
            btn.GetComponentInChildren<TMP_Text>().SetText($"Level {level += 1}");
        }
    }


    void Update()
    {

    }
}

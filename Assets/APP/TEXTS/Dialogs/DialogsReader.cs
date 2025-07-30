using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogsReader : MonoBehaviour
{
    

    [Header("___GRAPH___")]
    public DialogGraph current_dialog;


    void OnEnable()
    {
        EventBus.act_Speach += ViewSpeach;
    }
    void OnDisable()
    {
        EventBus.act_Speach -= ViewSpeach;
    }
    void Start()
    {

        current_dialog.Starting();
    }
    void ViewSpeach(string _name, string _text)
    {
        Debug.Log(_name + " : " + _text);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventBus.act_Next_Speach?.Invoke();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogsReader : MonoBehaviour
{
    

    [Header("___GRAPH___")]
    public DialogGraph current_dialog;
    [Header("___UI___")]
    [SerializeField] LocalizedText txt_text;
    [SerializeField] LocalizedText txt_name;
    [SerializeField] ContentSizeFitter size_txt_name;

    [SerializeField] GameObject canvas;


    void OnEnable()
    {
        EventBus.act_Speach += ViewSpeach;
        EventBus.act_Start_Dialogue += Start_Dialog;
        EventBus.act_End_Dialogue += End_Dialog;
    }
    void OnDisable()
    {
        EventBus.act_Speach -= ViewSpeach;
        EventBus.act_Start_Dialogue -= Start_Dialog;
        EventBus.act_End_Dialogue -= End_Dialog;
    }
    void Start_Dialog(DialogGraph _graph)
    {
        InputMapSwitcher.Switch(InputMapSwitcher.Maps.UI);
        canvas.SetActive(true);
        current_dialog = _graph;
        current_dialog.Starting();
    }
    void End_Dialog()
    {
        InputMapSwitcher.Switch(InputMapSwitcher.Maps.Gameplay);
        current_dialog = null;
        canvas.SetActive(false);
    }
    async void ViewSpeach(string _name, string _text)
    {
        Debug.Log(_name + " : " + _text);
        //txt_text.text = _text;
        txt_text.ChangeText_To("Dialog."+current_dialog.name+"."+_text);
        txt_name.ChangeText_To("Speacker." + _name);
        await Task.Delay(10);
        size_txt_name.enabled = false;
        size_txt_name.enabled = true;
    }
    public void btn_NextSpeach()
    {
        Debug.Log("---next---");
        EventBus.act_Next_Speach?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] GameObject canvas, obj_btn_next;

    [SerializeField] Transform trans_choices;
    List<GameObject> objs_choice_button;


    void OnEnable()
    {
        EventBus.act_Speach += ViewSpeach;
        EventBus.act_Start_Dialogue += Start_Dialog;
        EventBus.act_End_Dialogue += End_Dialog;
        EventBus.act_Choice += Choice_Visualizer;
    }
    void OnDisable()
    {
        EventBus.act_Speach -= ViewSpeach;
        EventBus.act_Start_Dialogue -= Start_Dialog;
        EventBus.act_End_Dialogue -= End_Dialog;
        EventBus.act_Choice -= Choice_Visualizer;
    }
    void Start_Dialog(DialogGraph _graph)
    {
        objs_choice_button = new List<GameObject>();
        foreach (ChoiceButton b in trans_choices.GetComponentsInChildren<ChoiceButton>())
        {
            objs_choice_button.Add(b.gameObject);
            Debug.Log("add btn "+b.gameObject.name);
        }
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
        obj_btn_next.SetActive(true);
        trans_choices.gameObject.SetActive(false);
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
    void Choice_Visualizer(string[] _var)
    {
        foreach (string s in _var)
        {
            Debug.Log(s);
        }
        obj_btn_next.SetActive(false);
        trans_choices.gameObject.SetActive(true);
        for (int i = 0; i < objs_choice_button.Count; i++)
        {
            Debug.Log(i < _var.Length);
            if (i < _var.Length)
            {
                
                objs_choice_button[i].gameObject.SetActive(true);
                objs_choice_button[i].GetComponentInChildren<LocalizedText>().ChangeText_To(_var[i]);
            }
            else objs_choice_button[i].gameObject.SetActive(false);


            //trans_choices.GetChild(i).gameObject.SetActive(i<_var.Length);
        }
    }

}

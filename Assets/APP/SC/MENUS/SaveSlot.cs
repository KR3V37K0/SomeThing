using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] TMP_Text txt_name, txt_time;
    [SerializeField] GameObject obj_newgame;
    [SerializeField] Button btn_load, btn_delete;
    SavePreview preview;
    public void SetData(SavePreview _preview, Func<SavePreview, Task> func_btn_load,Func<SavePreview, Task> func_btn_delete)
    {
        preview = _preview;
        if (_preview.is_New)
        {
            txt_time.gameObject.SetActive(false);
            txt_name.gameObject.SetActive(false);
            btn_delete.gameObject.SetActive(false);
            obj_newgame.SetActive(true);
        }
        else
        {
            txt_time.gameObject.SetActive(true);
            txt_name.gameObject.SetActive(true);
            obj_newgame.SetActive(false);
            txt_name.text = _preview.file_name;
            txt_time.text = (_preview.time_minutes / 60) + ":" + (_preview.time_minutes % 60);
            btn_delete.gameObject.SetActive(true);
            btn_delete.onClick.AddListener(() => func_btn_delete(_preview));
        }
        //btn_load.onClick.AddListener(() => MainMenu.btn_loadSave(_preview));
        btn_load.onClick.AddListener(() => func_btn_load(_preview));
        

    }
}

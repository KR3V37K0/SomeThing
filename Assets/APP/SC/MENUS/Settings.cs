using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [OdinSerialize] SettingsData new_settings= new SettingsData();
    [Header("___LANGUAGE___")]
    [SerializeField] Transform trans_languages;

    [Header("__CAMERA___")]
    [SerializeField] TMP_Text txt_camera_sensetive_value;
    [SerializeField] Slider slider_camera_sense;

    void Awake()
    {
        //gameObject.SetActive(false);
    }
    void OnEnable()
    {
        new_settings = JsonUtility.FromJson<SettingsData>(JsonUtility.ToJson(SaveLoad.current_settings));

        trans_languages.Find("btn_" + new_settings.language).GetComponent<Button>().interactable = false;

        slider_camera_sense.value=new_settings.camera_sensetive;
        txt_camera_sensetive_value.text = slider_camera_sense.value.ToString("0.00");


    }
    public void btn_Cancel()
    {
        btn_SetLanguage(SaveLoad.current_settings.language);
    }
    public void btn_Apply()
    {
        SaveLoad.current_settings = new_settings;
        SaveLoad.SaveSettings();
    }
    public void btn_SetLanguage(string key)
    {
        trans_languages.Find("btn_" + new_settings.language).GetComponent<Button>().interactable = true;
        trans_languages.Find("btn_" + key).GetComponent<Button>().interactable = false;
        Localization.SetLanguage(key);
        new_settings.language = key;
    }
    public void slider_SetCameraSensetive()
    {
        new_settings.camera_sensetive = slider_camera_sense.value;
        txt_camera_sensetive_value.text= slider_camera_sense.value.ToString("0.00");
    }


}


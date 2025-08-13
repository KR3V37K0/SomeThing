using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    SettingsData new_settings= new SettingsData();
    [Header("___COMMON___")]
    [SerializeField] GameObject[] panels_settings;

    [Header("___LANGUAGE___")]
    [SerializeField] Transform trans_languages;

    [Header("___CAMERA___")]
    [SerializeField] TMP_Text txt_camera_sensetive_value;
    [SerializeField] Slider slider_camera_sense;

    [Header("___WINDOW___")]
    [SerializeField] Button[] btn_windows;

    [Header("___GRAPHICS___")]
    [SerializeField] TMP_Text txt_FPS_value;
    [SerializeField] Slider slider_FPS;
    [SerializeField] Toggle toggle_vsync;

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



        btn_change_WindowMode(SaveLoad.current_settings.window_mode);

        slider_FPS.value= SaveLoad.current_settings.fps;
        slider_SetFPS();

        btn_Quolity(SaveLoad.current_settings.quality_graphics);

        if (SaveLoad.current_settings.vsync) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
        toggle_vsync.isOn = SaveLoad.current_settings.vsync;


        btn_swap_panel(0);

    }
    public void btn_Cancel()
    {
        btn_SetLanguage(SaveLoad.current_settings.language);
        btn_change_WindowMode(SaveLoad.current_settings.window_mode);
        //TODO: returning

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
    public void btn_swap_panel(int _panel)
    {
        for (int i = 0; i < panels_settings.Length; i++)
        {
            panels_settings[i].SetActive(i == _panel);
        }

    }


    //GRAPHICS
    public void btn_change_WindowMode(int _mode)
    {
        // 0 = noFrame
        // 1 = fulscreen
        // 2 = windowed
        for (int i = 0; i < btn_windows.Length; i++)
        {
            btn_windows[i].interactable = _mode != i;
        }
        switch (_mode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
        new_settings.window_mode = _mode;

    }
    public void slider_SetFPS()
    {
        new_settings.fps = (int)slider_FPS.value;
        txt_FPS_value.text = ((int)slider_FPS.value).ToString("000");
        Application.targetFrameRate= (int)slider_FPS.value;
    }
    public void btn_Quolity(int value)
    {
        new_settings.quality_graphics = value;
        QualitySettings.SetQualityLevel(value, true);
    }
    public void check_vsync()
    {
        new_settings.vsync = toggle_vsync.isOn;
        if (toggle_vsync.isOn) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
    }
}


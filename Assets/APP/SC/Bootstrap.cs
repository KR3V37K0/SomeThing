using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    async void Start()
    {
        //PlayerPrefs.DeleteAll();
        await SaveLoad.LoadSetting();
        Localization.Init(); // не работает в awake
    }
}

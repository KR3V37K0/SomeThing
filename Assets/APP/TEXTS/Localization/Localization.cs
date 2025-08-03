using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Linq;
using Mono.Data.Sqlite;
using System.Threading;
using System.Data;
public static class Localization
{
    [Header("___LANGUAGES___")]
    //private static Dictionary<string, string> _localizedTexts = new();
    public static Dictionary<int, string> languages { get; private set; } = new Dictionary<int, string>
    {
        {1,"eng"},
        {2,"ru"}
    };
    public static int current_language_code { get; private set; }


    public static void Init()
    {
        // if (PlayerPrefs.HasKey("current_language")) current_language_code = languages.FirstOrDefault(x => x.Value == PlayerPrefs.GetString("current_language")).Key;
        // else { PlayerPrefs.SetString("current_language", "eng"); current_language_code = 1; }

        current_language_code = languages.FirstOrDefault(x => x.Value == SaveLoad.current_settings.language).Key;


        EventBus.act_Language_Changed?.Invoke();
    }
    public static async Task<string> Get(string keyword)
    {
        return await DatabaseCommands.GetLocalization(keyword);
    }

    public static void SetLanguage(string key)
    {
       // PlayerPrefs.SetString("current_language", key);
        current_language_code = languages.FirstOrDefault(x => x.Value == key).Key;
        //LoadFromCSV(csv);
        EventBus.act_Language_Changed?.Invoke();
    }
}


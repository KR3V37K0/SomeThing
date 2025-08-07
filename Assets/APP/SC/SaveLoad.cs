using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
//using UnityEngine.WSA;

public static class SaveLoad
{
    public static SaveData current_save;
    public static SettingsData current_settings;

    public static bool inLoading = false;
    async public static Task Save()
    {
        inLoading = true;
        PlayerPrefs.SetString("save_" + current_save.preview.slot,
            JsonUtility.ToJson(current_save)
        );
        PlayerPrefs.Save();
        inLoading = false;
    }
    async public static Task CreateNewSave(SavePreview _preview)
    {
        inLoading = true;
        current_save = new SaveData(_preview);
        current_save.preview.is_New = false;
        await Save();
        inLoading = false;
    }
    async public static Task Load(SavePreview _preview)
    {
        inLoading = true;
        current_save = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("save_" + _preview.slot));
        inLoading = false;
    }
    async public static Task<List<SavePreview>> GetAllSavePreview()
    {
        inLoading = true;
        List<SavePreview> previews = new List<SavePreview>();
        for (int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.HasKey("save_" + i))
            {
                previews.Add(
                    JsonUtility.FromJson<SaveData>(
                        PlayerPrefs.GetString("save_" + i)
                    ).preview
                );
            }
            else
            {

                previews.Add(
                    new SavePreview("save_" + i, i, 0, true)
                );
            }
        }
        inLoading = false;
        return previews;
    }
    async public static Task DeleteSave(SavePreview _preview) {
        PlayerPrefs.DeleteKey("save_" + _preview.slot);
        PlayerPrefs.Save();
    }



    async public static Task LoadSetting()
    {
        if (PlayerPrefs.HasKey("settings"))
        {
            current_settings= JsonUtility.FromJson<SettingsData>(PlayerPrefs.GetString("settings"));
        }
        else
        {
            current_settings=new SettingsData();
            SaveSettings();
        }
    }
    async public static Task SaveSettings()
    {
        PlayerPrefs.SetString("settings",
            JsonUtility.ToJson(current_settings)
        );
    }
}


[System.Serializable]
public class SavePreview
{
    public string file_name;
    public int slot;
    public int time_minutes;
    public bool is_New;
    public SavePreview(string _file_name, int _slot, int _time_minutes, bool _is_New)
    {
        file_name = _file_name;
        slot = _slot;
        time_minutes = _time_minutes;
        is_New = _is_New;
    }
}

[System.Serializable]
public class SaveData
{
    public SavePreview preview;
    public string location;

    public SaveData(SavePreview _preview)
    {
        preview = _preview;
    }
}

[System.Serializable]
public class SettingsData
{
    public string language;
    public float camera_sensetive;
    public int window_mode;

    public SettingsData(string _language, float _camera_sensetive, int window_mode)
    {
        language = _language;
        camera_sensetive = _camera_sensetive;
        this.window_mode = window_mode;
    }
    public SettingsData() 
    { 
        language="eng";
        camera_sensetive = 1;
        window_mode = 0;
    }
}

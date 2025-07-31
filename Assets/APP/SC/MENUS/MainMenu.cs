using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panel_save_load;
    [SerializeField] Transform trans_slots;
    void Start()
    {
        Transition.Instance.FadeOut();
        panel_save_load.SetActive(false);
    }
    async public void btn_Start()
    {
        //PlayerPrefs.DeleteAll();

        foreach (SavePreview _preview in await SaveLoad.GetAllSavePreview())
        {
            trans_slots.GetChild(_preview.slot - 1).GetComponent<SaveSlot>().SetData(_preview, btn_LoadSave, btn_DeleteSave);
        }


        panel_save_load.SetActive(true);


    }
    /*public static async Task btn_LoadSave(SavePreview _preview)
    {
        if (_preview.is_New) SaveLoad.CreateNewSave(_preview);
        else SaveLoad.Load(_preview);

        LoadScene.StartLoading("HUB");
    }*/
    public async Task btn_LoadSave(SavePreview _preview)
    {
        if (_preview.is_New) SaveLoad.CreateNewSave(_preview);
        else SaveLoad.Load(_preview);

        await Transition.Instance.FadeIn();
        LoadScene.StartLoading("HUB");
    }
    public async Task btn_DeleteSave(SavePreview _preview)
    {
        await SaveLoad.DeleteSave(_preview);
        
        btn_Start();
    }
}


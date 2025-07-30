using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Transform trans_languages;

    void Awake()
    {
        //gameObject.SetActive(false);
    }
    void OnEnable()
    {
        //Languages set
        if (PlayerPrefs.HasKey("current_language"))
            trans_languages.Find("btn_" + PlayerPrefs.GetString("current_language")).GetComponent<Button>().interactable = false;
    }
    public void btn_SetLanguage(string key)
    {
        trans_languages.Find("btn_" + PlayerPrefs.GetString("current_language")).GetComponent<Button>().interactable = true;
        trans_languages.Find("btn_" + key).GetComponent<Button>().interactable = false;
        Localization.SetLanguage(key);
    }
}

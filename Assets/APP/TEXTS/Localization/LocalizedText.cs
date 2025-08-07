using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class LocalizedText : MonoBehaviour
{
    TMP_Text txt;
    string keyword;
    void OnEnable()
    {
        EventBus.act_Language_Changed += LoadText;
    }
    void OnDisable()
    {
        EventBus.act_Language_Changed -= LoadText;
    }
    async void Start()
    {
        txt = GetComponent<TMP_Text>();
        keyword = txt.text;
        await Task.Delay(50);
        LoadText();
    }
    async public void LoadText()
    {
        while (keyword == null)
        {
            await Task.Delay(100);
        }
        txt.text = await Localization.Get(keyword);
    }
    public void ChangeText_To(string _keyword)
    {
        keyword = _keyword;
        LoadText();
    }
    
}

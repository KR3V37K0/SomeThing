using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.UI;
using System.Threading.Tasks;
using TMPro;

public class LoadScene : MonoBehaviour
{
    [Header("___PROGRESS BAR___")]
    [SerializeField] Image img_slider;
    [SerializeField] GameObject obj_complete;
    [SerializeField] float load_speed, current_load = 0;

    [Header("___TIPS___")]
    [SerializeField] LocalizedText txt_tip;
    [SerializeField] int time_to_read;
    Task generator;


    static string loaded_scene;
    AsyncOperation loader;


    async void Start()
    {
        loader = SceneManager.LoadSceneAsync(loaded_scene);
        loader.allowSceneActivation = false;

        obj_complete.SetActive(false);
        generator=TipsGenerator();
    }
    void Update()
    {
        img_slider.fillAmount = current_load / 0.9f;
        if (current_load < loader.progress)
        {
            if (SaveLoad.inLoading) current_load += load_speed * Time.deltaTime * 0.5f;
            else current_load += load_speed * Time.deltaTime;
        }
        else if (!obj_complete.activeSelf && loader.progress >= 0.9f) obj_complete.SetActive(true);

    }
    public static void StartLoading(string scene_name)
    {
        loaded_scene = scene_name;
        SceneManager.LoadScene("LOADING");
    }
    public void btn_continue()
    {
        generator.Dispose();
        loader.allowSceneActivation = true;
    }
    async Task TipsGenerator()
    {
        while (true)
        {
            txt_tip.ChangeText_To(await DatabaseCommands.GetRandomTipsKeyword());

            await Task.Delay(time_to_read*1000);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    bool in_generation = false;

    [Header("___SCENE___")]
    static string loaded_scene;
    GameObject obj_env;
    //[SerializeField] Image transition;
    AsyncOperation loader;


    async void Start()
    {
#if UNITY_EDITOR
        load_speed = load_speed * 3;
#endif
        loader = SceneManager.LoadSceneAsync(loaded_scene);
        loader.allowSceneActivation = false;

        StartCoroutine( InstantiateEnviroment());
        obj_complete.SetActive(false);
        in_generation = true;
        generator=TipsGenerator();

        Transition.Instance.FadeOut();

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
    public async void btn_continue()
    {
        await Transition.Instance.FadeIn();
        in_generation =false;
        Destroy(obj_env);
        Resources.UnloadUnusedAssets();
        loader.allowSceneActivation = true;
        Transition.Instance.FadeOut();

    }
    async Task TipsGenerator()
    {
        while (in_generation)
        {
            txt_tip.ChangeText_To(await DatabaseCommands.GetRandomTipsKeyword());

            await Task.Delay(time_to_read*1000);
        }
        return;
    }


    IEnumerator InstantiateEnviroment()
    {
        var handle = Resources.Load("LoadingScenes/LoadingScene_1");
        yield return handle;
        obj_env =Instantiate(handle as GameObject);
        obj_env.transform.position = new Vector3(0,0,0);


        //TODO: тут короче сцена будет черная. а при загрузке освещение включится.
    }

}

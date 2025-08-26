using UnityEngine;

public class EnterScene : MonoBehaviour
{
    [SerializeField] string scene_name;
    bool isTriggered = false;
    async void OnTriggerEnter(Collider col)
    {
        if (isTriggered) return;
        if (col.tag != "Player") return;
        isTriggered = true;
        await Transition.Instance.FadeIn();
        LoadScene.StartLoading(scene_name);
    }
}

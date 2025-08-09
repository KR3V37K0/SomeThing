using UnityEngine;

public class Esc : MonoBehaviour
{
    [SerializeField]GameObject canvas;
    public async void btn_to_main_menu()
    {
        await Transition.Instance.FadeIn();
        LoadScene.StartLoading("MAIN_MENU");
    }
    private void OnEnable()
    {
        InputMapSwitcher.Switch(InputMapSwitcher.Maps.UI);
    }
    private void OnDisable()
    {
        InputMapSwitcher.Switch(InputMapSwitcher.Maps.Gameplay);
    }
}

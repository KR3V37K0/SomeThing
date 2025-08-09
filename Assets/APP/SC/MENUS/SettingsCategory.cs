using UnityEngine;
using UnityEngine.UI;

public class SettingsCategory : MonoBehaviour
{
    [SerializeField] Button btn_my;
    [SerializeField] GameObject obj_select;
    private void OnEnable()
    {
        btn_my.interactable = false;
        //TODO:SELECTION
    }
    private void OnDisable()
    {
        btn_my.interactable = true;
    }
}

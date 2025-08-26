using UnityEngine;

public class TEST_Talk_NPC : MonoBehaviour, IInteractable
{
    [SerializeField] DialogGraph dialog;
    [SerializeField] CamerasManager cameras;
    void Awake()
    {
        cameras.enabled = false;
    }
    void OnEnable()
    {
        EventBus.act_End_Dialogue += StopCameras;
    }
    void OnDisable()
    {
        EventBus.act_End_Dialogue -= StopCameras;
    }
    public void OnInteract()
    {
        cameras.enabled = true;
        EventBus.act_Start_Dialogue?.Invoke(dialog);      
    }
    void StopCameras()
    {
        cameras.enabled = false;
    }
}

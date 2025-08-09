using UnityEngine;

public class TEST_Talk_NPC : MonoBehaviour,IInteractable
{
    [SerializeField] DialogGraph dialog;
    public void OnInteract()
    {
        Debug.Log("START");
        EventBus.act_Start_Dialogue?.Invoke(dialog);
    }
}

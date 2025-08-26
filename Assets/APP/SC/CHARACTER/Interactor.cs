using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] GameObject obj_txt_interact;
    [SerializeField] TMP_Text txt_button;
    [SerializeField] Image img_button;
    bool inInteraction = false;
    IInteractable i;
    private void OnEnable()
    {
        EventBus.act_End_Dialogue += End_Interaction;
    }
    private void OnDisable()
    {
        EventBus.act_End_Dialogue -= End_Interaction;
    }
    private void Update()
    {
         obj_txt_interact.SetActive(i != null && inInteraction==false);
    }
    private void OnTriggerStay(Collider _coll)
    {
        i = _coll.gameObject.GetComponentInChildren<IInteractable>();
    }
    private void OnTriggerExit(Collider _coll)
    {
        i = null;
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (i!=null)
        {
            if (context.phase == InputActionPhase.Started)
            {
                i.OnInteract();
                inInteraction = true;
            }
        } 
    }
    void End_Interaction()
    {
        inInteraction = false;
    }
}

public interface IInteractable
{
    void OnInteract();
}

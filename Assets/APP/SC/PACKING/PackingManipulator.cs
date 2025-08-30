using UnityEngine;
using UnityEngine.InputSystem;
public class PackingManipulator : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject view_camera;
    [SerializeField] Graber graber;
    [SerializeField] PlayerInput input;
    InputActionMap map;
    void Awake()
    {
        view_camera.SetActive(false);
        graber.enabled = false;
        map = input.actions.FindActionMap("Packing");
    }
    public void OnInteract()
    {
        view_camera.SetActive(true);
        graber.enabled = true;

        InputMapSwitcher.Switch(InputMapSwitcher.Maps.Packing);

        map.FindAction("Esc").performed += OnEsc;
    }
    public void OnEsc(InputAction.CallbackContext ctx)
    {
        map.FindAction("Esc").performed -= OnEsc;

        InputMapSwitcher.Switch(InputMapSwitcher.Maps.Gameplay);

        view_camera.SetActive(false);
        graber.enabled = false;
    }
}

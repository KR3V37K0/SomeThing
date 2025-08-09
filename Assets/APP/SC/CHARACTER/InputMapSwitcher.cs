using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapSwitcher : MonoBehaviour
{
    public static PlayerInput Input { get; private set; }
    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }
    public enum Maps
    {
        UI,
        Gameplay
    }
    public static void Switch(Maps _map)
    {
        Input.SwitchCurrentActionMap(_map.ToString());
    }
}

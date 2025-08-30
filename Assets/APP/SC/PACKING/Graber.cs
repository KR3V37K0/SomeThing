using UnityEngine;
using UnityEngine.InputSystem;

public class Graber : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    InputActionMap map;
    Grid grid;
    GameObject obj_grabed;
    void Awake()
    {
        map = input.actions.FindActionMap("Packing");
    }
    void OnEnable()
    {
        map.FindAction("Grab").performed += OnGrab;
        map.FindAction("MousePosition").performed += OnMouse;
        map.FindAction("Rotate").performed += Rotate;
    }
    void OnDisable()
    {
        map.FindAction("Grab").performed -= OnGrab;
        map.FindAction("MousePosition").performed -= OnMouse;
        map.FindAction("Rotate").performed -= Rotate;
    }
    public void OnGrab(InputAction.CallbackContext ctx)
    {
        //ОТПУСКАЕМ
        if (obj_grabed != null)
        {
            obj_grabed.GetComponent<IGrabable>().OnRelease();
            obj_grabed = null;
        }
        // ИЛИ БЕРЕМ
        else
        {
            //Debug.Log("2.2");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, ~LayerMask.GetMask("Grid")))
            {
                if (hit.collider.gameObject.GetComponentInParent<IGrabable>() != null)
                {

                    obj_grabed = hit.collider.gameObject.transform.parent.gameObject;
                    obj_grabed.GetComponent<IGrabable>().OnGrab(this);
                    //Debug.Log(obj_grabed.name);
                }
            }
        }

        

    }
    Ray ray;
    RaycastHit hit;
    public void OnMouse(InputAction.CallbackContext ctx)
    {
        if (obj_grabed == null) return;

        //ДВИГАЕМ ВЗЯТЫЙ ОБЪЕКТ
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // СВОБОДНО
            if (grid == null)
            {
                obj_grabed.transform.position = hit.point;
            }
            // ПО СЕТКЕ
            else
            {
                obj_grabed.transform.position = grid.GetCellCenterWorld( grid.WorldToCell(hit.point));            
            }
        }

    }

    Vector3 rotation_vector = new Vector3(0, 90, 0);
    void Rotate(InputAction.CallbackContext ctx)
    {
        if (obj_grabed == null) return;

        if(ctx.performed)
            obj_grabed.transform.Rotate(rotation_vector);
    }
    public void ExitGrid()
    {
        grid = null;
    }
    public void OnGrid(Grid _component)
    {
        grid = _component;
    }



}
public interface IGrabable
{
    public void OnGrab(Graber _graber)
    {

    }
    public void OnRelease()
    {
        
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using PrimeTween;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Graber : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    InputActionMap map;
    Grid grid;
    GameObject obj_grabed;
    [SerializeField] AnimationCurve _ease_rotate;
    static AnimationCurve ease_rotate;
    void Awake()
    {
        map = input.actions.FindActionMap("Packing");
        ease_rotate = _ease_rotate;
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
    void OnGrab(InputAction.CallbackContext ctx)
    {
        //ПРОВЕРЯЕМ НА ПЕРЕСЕЧЕНИЕ
        if (isCrossing) return;

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
    void OnMouse(InputAction.CallbackContext ctx)
    {
        if (obj_grabed == null) return;

        //ДВИГАЕМ ВЗЯТЫЙ ОБЪЕКТ
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,100, ~LayerMask.GetMask("Grabable")))
        {
            // СВОБОДНО
            if (grid == null)
            {
                Tween.Position(obj_grabed.transform, hit.point, 0.2f);
                //obj_grabed.transform.position = hit.point;
            }
            // ПО СЕТКЕ
            else
            {
                Tween.Position(obj_grabed.transform, grid.GetCellCenterWorld(grid.WorldToCell(hit.point)), 0.2f);
                //obj_grabed.transform.position = grid.GetCellCenterWorld( grid.WorldToCell(hit.point));            
            }
        }

    }

    Vector3 rotation_vector = new Vector3(0, 90, 0);
    bool inRotation = false;
    async void Rotate(InputAction.CallbackContext ctx)
    {
        if (inRotation)
        {
            return;
        }
        if (obj_grabed == null) return;

        if (ctx.performed)
        {
            inRotation = true;
            await Tween.Rotation(obj_grabed.transform, obj_grabed.transform.rotation.eulerAngles + rotation_vector, 0.4f, ease_rotate);
            inRotation = false;
        }
    }
    public void ExitGrid()
    {
        grid = null;
    }
    public void OnGrid(Grid _component)
    {
        grid = _component;
    }

    bool isCrossing = false;
    public void OnCrossing(bool _bool)
    {

        if (_bool != isCrossing) ChangeObjCrossColor();
        isCrossing = _bool;

    }
    void ChangeObjCrossColor()
    {
        if (isCrossing)
        {
            foreach (Renderer rend in obj_grabed.GetComponentsInChildren<Renderer>())
            {
                rend.material.SetFloat("_Tinting", 0);
            }
        }
        else
        {
            foreach (Renderer rend in obj_grabed.GetComponentsInChildren<Renderer>())
            {
                rend.material.SetFloat("_Tinting", 1);
            }
            
        }
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

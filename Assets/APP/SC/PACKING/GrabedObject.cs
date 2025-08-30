using UnityEngine;

public class GrabedObject : MonoBehaviour, IGrabable
{
    Graber graber;
    [SerializeField] Rigidbody rb;
    public void OnGrab(Graber _graber)
    {
        graber = _graber;
        transform.GetChild(0).gameObject.layer = 2;
        rb.isKinematic = true;
    }
    public void OnRelease()
    {
        graber = null;
        rb.isKinematic = false;
        transform.GetChild(0).gameObject.layer = 6;
    }
    void OnTriggerStay(Collider col)
    {
        if (graber != null)
        {
            if (col.gameObject.tag == "grid")
            {
                graber.OnGrid(col.gameObject.GetComponentInParent<Grid>());
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        
        if (graber != null)
        {
            if (col.gameObject.tag == "grid")
            {
                graber.ExitGrid();
            }
        }
    }

}

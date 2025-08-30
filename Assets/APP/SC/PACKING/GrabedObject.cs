using UnityEngine;

public class GrabedObject : MonoBehaviour, IGrabable
{
    Graber graber;
    [SerializeField] Rigidbody rb;
    public void OnGrab(Graber _graber)
    {
        graber = _graber;
        foreach (BoxCollider box in GetComponentsInChildren<BoxCollider>())
        {
            box.isTrigger = true;
        }
        rb.isKinematic = true;
    }
    public void OnRelease()
    {
        graber = null;
        rb.isKinematic = false;
        foreach (BoxCollider box in GetComponentsInChildren<BoxCollider>())
        {
            box.isTrigger = false;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (graber != null)
        {
            if (col.gameObject.tag == "grid")
            {
                graber.OnGrid(col.gameObject.GetComponentInParent<Grid>());
            }
            else if (col.gameObject.layer == 6)
            {
                graber.OnCrossing(true);
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
            else if (col.gameObject.layer == 6)
            {
                graber.OnCrossing(false);
            }
        }
    }

}

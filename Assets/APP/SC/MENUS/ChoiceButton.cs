using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public void btn_Choice()
    {
        //Debug.Log("CLICK + " + gameObject.name[9]);
        EventBus.act_Select_Choice?.Invoke(int.Parse(gameObject.name[9]+""));
    }
    
}

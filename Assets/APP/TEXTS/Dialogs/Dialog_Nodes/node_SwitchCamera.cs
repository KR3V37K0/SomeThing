using UnityEngine;

public class node_SwitchCamera : Base
{
    [SerializeField] int camera;

    public override void Execute()
    {
        EventBus.act_Switch_Camera?.Invoke(camera);
        EventBus.act_Next_Speach?.Invoke();
        base.Execute();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogGraph : NodeGraph
{
    public Base start;
    public Base current;

    public void Starting()
    {
        Subscrible(true);
        current = start;
        current.Execute();
    }
    void Subscrible(bool b)
    {
        if(b) EventBus.act_Next_Speach += NextSpeach;
        else EventBus.act_Next_Speach -= NextSpeach;
    }
    void NextSpeach()
    {
        current = current.GetOutputPort("Out").Connection.node as Base;
        if(current is node_End) Subscrible(false);
        current.Execute();
    }
}

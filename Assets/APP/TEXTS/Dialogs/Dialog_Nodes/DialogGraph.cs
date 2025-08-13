using System;
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
    public void Ending()
    {
        Subscrible(false);
    }
    void Subscrible(bool b)
    {
        if (b)
        {
            EventBus.act_Next_Speach += NextSpeach;
            EventBus.act_Select_Choice += SelectChoice;
        }
        else 
        { 
            EventBus.act_Next_Speach -= NextSpeach;
            EventBus.act_Select_Choice -= SelectChoice;
        }
    }
    void NextSpeach()
    {

        current = current.GetOutputPort("Out").Connection.node as Base;
        if(current is node_End) Subscrible(false);

        current.Execute();
    }   
    void SelectChoice(int _index)
    {
        Debug.Log("get signal " + _index);
        current = current.GetOutputPort($"Answer {_index}").Connection.node as Base;
        if (current is node_End) Subscrible(false);

        current.Execute();
        /*NodePort port = current.GetOutputPort($"Answer {_index}");
        if (port != null && port.IsConnected)
        {
            (port.Connection.node as Base)?.Execute();
        }*/
    }
    
}

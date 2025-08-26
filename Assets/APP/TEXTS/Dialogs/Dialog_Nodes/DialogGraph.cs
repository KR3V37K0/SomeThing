using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
            EventBus.act_End_Dialogue += Ending;
        }
        else
        {
            EventBus.act_Next_Speach -= NextSpeach;
            EventBus.act_Select_Choice -= SelectChoice;
            EventBus.act_End_Dialogue -= Ending;
        }
    }
    void NextSpeach()
    {
        current = current.GetOutputPort("Out").Connection.node as Base;

        current.Execute();
    }
    void SelectChoice(int _index)
    {
        current = current.GetOutputPort($"Answer {_index}").Connection.node as Base;

        current.Execute();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogGraph : NodeGraph
{
    public Base start;
    public Base current;

    public void Starting()
    {
        Debug.Log("working");
        EventBus.act_Next_Speach += NextSpeach;
        current = start;
        current.Execute();
    }
    void NextSpeach()
    {
        current = current.GetOutputPort("Out").Connection.node as Base;
        current.Execute();
    }
}

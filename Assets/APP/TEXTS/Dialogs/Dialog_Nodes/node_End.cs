using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node_End : Base
{
    public override void Execute()
    {
        EventBus.act_End_Dialogue?.Invoke();
    }
}

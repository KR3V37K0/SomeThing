using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node_Start : Base
{
    public override void Execute()
    {
        EventBus.act_Next_Speach?.Invoke();
    }
}

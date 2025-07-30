using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Base : Node
{
    [Input] public bool In;
    [Output] public bool Out;

    public virtual void Execute() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBus
{
    public static Action<string, string> act_Speach;
    public static Action act_Next_Speach;
    public static Action act_End_Dialogue;
    public static Action act_Language_Changed;
    public static Action<DialogGraph> act_Start_Dialogue;
}

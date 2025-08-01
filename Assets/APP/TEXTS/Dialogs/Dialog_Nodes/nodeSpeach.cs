using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class nodeSpeach : Base
{
    public string[] speacker;
    public string[] text;

    public override void Execute()
    {
        EventBus.act_Speach?.Invoke(speacker[Localization.current_language_code], text[Localization.current_language_code]);
        base.Execute();
    }
}

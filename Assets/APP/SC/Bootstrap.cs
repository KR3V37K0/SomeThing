using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        Localization.Init(); // не работает в awake
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HD.FrameworkDesign;
using System.Runtime.InteropServices;

public class Gameaa : MonoBehaviour
{
    private void Start()
    {
        var scriptA = ContainerModel.Get<ScriptA>();
        scriptA.Print();
    }
}

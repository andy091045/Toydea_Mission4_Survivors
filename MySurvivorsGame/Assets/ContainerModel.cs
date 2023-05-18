using HD.FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerModel : Architecture<ContainerModel>
{
    protected override void Init()
    {
        Register(new ScriptA());
    }
}

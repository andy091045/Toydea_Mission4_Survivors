using DataProcess;
using HD.FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : Architecture<DataContainer>
{
    protected override void Init()
    {
        Register(new ExcelReadWrite());
        Register(new DataInit());
    }
}

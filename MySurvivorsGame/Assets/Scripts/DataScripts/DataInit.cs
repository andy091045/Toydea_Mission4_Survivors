using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataProcess;
using System.IO;
using System;
using System.Runtime.Serialization;

public class DataInit : MonoBehaviour
{
    public DatasPathList datasPathList = new DatasPathList();
    public abc A = new abc();

    void Start()
    {        
        ExcelReadWrite excelReadWrite = new ExcelReadWrite();

        string excelPath = Path.Combine(Application.streamingAssetsPath, "DatasPath.xlsx");
        string excelSheetName = "DatasPath";
        var excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);

        datasPathList.datasPath = excelReadWrite.ParseListDataJson<DatasPath>(excelRowData);

        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, datasPathList.datasPath[2].Path);
        excelSheetName = "RealTimePlayerData";
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        //Debug.Log(excelRowData[1][0].ToString());
        datasPathList.realTimePlayerData = excelReadWrite.ParseDataJson<RealTimePlayerData>(excelRowData);
    }

}

[Serializable]
public class abc
{
    public List<int> a;
}

[Serializable]
public class DatasPathList
{
    public List<DatasPath> datasPath = new List<DatasPath>();
    public RealTimeData realTimeData = new RealTimeData();
    public RealTimePlayerData realTimePlayerData = new RealTimePlayerData();
}

[Serializable]
public class DatasPath
{
    [field:SerializeField] public string Name { get; set; }
    [field: SerializeField] public string Path { get; set; }
}

[Serializable]
public class RealTimeData
{
    [field: SerializeField] public float Volume { get; set; }
    [field: SerializeField] public int KillNumber { get; set; }
}

[Serializable]
public class RealTimePlayerData
{
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float Exp { get; set; }
    [field: SerializeField] public float AbsorbExpRange { get; set; }
    [field: SerializeField] public float DemageCut { get; set; }
    [field: SerializeField] public float Recovery { get; set; }
    [field: SerializeField] public float DropRate { get; set; }
    [field: SerializeField] public int SoulNumber { get; set; }
}


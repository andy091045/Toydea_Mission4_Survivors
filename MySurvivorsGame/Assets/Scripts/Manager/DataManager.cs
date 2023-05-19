using DataProcess;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    void Start()
    {
        var dataInit = DataContainer.Get<DataInit>();
        var excelTool = DataContainer.Get<ExcelReadWrite>();

        var excelPath = Path.Combine(Application.streamingAssetsPath, "RealTimeData.xlsx");
        //dataInit.dataGroup.realTimeData.KillNumber = 10;
        Debug.Log(dataInit.dataGroup.realTimeData.KillNumber);
        //excelTool.WriteExcelData<RealTimeData>(excelPath, "RealTimeData", dataInit.dataGroup.realTimeData);
    }

    public void ChooseDevil(string DevilName)
    {
        Debug.Log("ëIù¢" + DevilName);
    }
}

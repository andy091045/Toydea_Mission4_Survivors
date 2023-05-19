using DataProcess;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    DataReadStore dataReadStore_;
    void Start()
    {
        dataReadStore_ = DataContainer.Get<DataReadStore>();
        var excelTool = DataContainer.Get<ExcelReadWrite>();

        var excelPath = Path.Combine(Application.streamingAssetsPath, "RealTimeData.xlsx");
    }

    // ChooseDevilScene----------------------------------------------------------------------------------------------------------------------
    public void ChooseDevil(string DevilName)
    {
        dataReadStore_.dataGroup.realTimePlayerData.ChooseDevil = DevilName;        
        Debug.Log("ëIù¢" + dataReadStore_.dataGroup.realTimePlayerData.ChooseDevil);
    }
}

using DataProcess;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    DataReadStore dataReadStore;
    void Start()
    {
        dataReadStore = DataContainer.Get<DataReadStore>();
        var excelTool = DataContainer.Get<ExcelReadWrite>();

        var excelPath = Path.Combine(Application.streamingAssetsPath, "RealTimeData.xlsx");
    }

    // ChooseDevilScene----------------------------------------------------------------------------------------------------------------------
    public void ChooseDevil(string DevilName)
    {
        dataReadStore.dataGroup.realTimePlayerData.ChooseDevil = DevilName;        
        Debug.Log("ëIù¢" + dataReadStore.dataGroup.realTimePlayerData.ChooseDevil);
    }
}

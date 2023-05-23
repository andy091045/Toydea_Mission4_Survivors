using DataDefinition;
using DataProcess;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public DataGroup dataGroup = new DataGroup();

    public void SetDataGroup()
    {
        ExcelReadWrite excelReadWrite = new ExcelReadWrite();

        string excelPath = Path.Combine(Application.streamingAssetsPath, "DatasPath.xlsx");
        string excelSheetName = "DatasPath";
        var excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);

        dataGroup.datasPath = excelReadWrite.ParseListDataJson<DatasPath>(excelRowData);

        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[0].Path);
        excelSheetName = dataGroup.datasPath[0].Name;
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.devilsData = excelReadWrite.ParseListDataJson<DevilData>(excelRowData);
        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[2].Path);
        excelSheetName = "RealTimeData";
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.realTimeData = excelReadWrite.ParseDataJson<RealTimeData>(excelRowData);
        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[3].Path);
        excelSheetName = "RealTimePlayerData";
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.realTimePlayerData = excelReadWrite.ParseDataJson<DevilData>(excelRowData);
    }

    public void StoreDataGroup()
    {
        ExcelReadWrite excelReadWrite = new ExcelReadWrite();

        string excelPath = Path.Combine(Application.streamingAssetsPath, "DatasPath.xlsx");
        string excelSheetName = "DatasPath";
        var excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);

        dataGroup.datasPath = excelReadWrite.ParseListDataJson<DatasPath>(excelRowData);

        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[3].Path);
        excelSheetName = "RealTimePlayerData";
        excelReadWrite.WriteExcelData<DevilData>(excelPath, excelSheetName, dataGroup.realTimePlayerData);
    }

    // ChooseDevilScene----------------------------------------------------------------------------------------------------------------------
    public void ChooseDevil(string DevilName)
    {
        Debug.Log(DevilName);
        dataGroup.realTimePlayerData.DevilName = DevilName;      
        
        if(DevilName == "Reaper")
        {
            dataGroup.realTimePlayerData.PrefabPath = "Assets/Prefabs/Devils/Reaper.prefab";
        }
        else if(DevilName == "BoneMan")
        {
            dataGroup.realTimePlayerData.PrefabPath = "Assets/Prefabs/Devils/BoneMan.prefab";
        }
        Debug.Log("ëIù¢" + dataGroup.realTimePlayerData.DevilName);
        Debug.Log("òHúl" + dataGroup.realTimePlayerData.PrefabPath);
    }
}

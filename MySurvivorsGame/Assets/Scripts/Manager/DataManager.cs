using DataDefinition;
using DataProcess;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

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
        //------------------------------------------------------------------------------------

        // WeaponData
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[4].Path);
        excelSheetName = dataGroup.datasPath[4].Name;
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.weaponsData = excelReadWrite.ParseListDataJson<WeaponData>(excelRowData);
        for(int i = 0; i < dataGroup.weaponsData.Count; i++)
        {
            excelRowData = excelReadWrite.ReadExcel(excelPath, dataGroup.weaponsData[i].SheetName);
            dataGroup.weaponsData[i].LevelList = excelReadWrite.ParseListDataJson<WeaponLevelData>(excelRowData);
        }
        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[5].Path);
        excelSheetName = dataGroup.datasPath[5].Name;
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.sceneProcessData = excelReadWrite.ParseListDataJson<SceneProcessData>(excelRowData);
        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[6].Path);
        excelSheetName = dataGroup.datasPath[6].Name;
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.npcsData = excelReadWrite.ParseListDataJson<NPCsData>(excelRowData);
        //------------------------------------------------------------------------------------

        //CrystalsData
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[7].Path);
        excelSheetName = dataGroup.datasPath[7].Name;
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.crystalsData = excelReadWrite.ParseListDataJson<CrystalsData>(excelRowData);
        //------------------------------------------------------------------------------------

        //LevelData
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[8].Path);
        excelSheetName = dataGroup.datasPath[8].Name;
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.levelData = excelReadWrite.ParseListDataJson<LevelData>(excelRowData);

        //------------------------------------------------------------------------------------

        // LevelData
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[9].Path);
        excelSheetName = dataGroup.datasPath[9].Name;
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.itemsData = excelReadWrite.ParseListDataJson<ItemData>(excelRowData);
        for (int i = 0; i < dataGroup.itemsData.Count; i++)
        {
            excelRowData = excelReadWrite.ReadExcel(excelPath, dataGroup.itemsData[i].SheetName);
            dataGroup.itemsData[i].LevelList = excelReadWrite.ParseListDataJson<ItemLevelData>(excelRowData);
        }
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
            dataGroup.realTimePlayerData = dataGroup.devilsData[1];
            Debug.Log(dataGroup.realTimePlayerData.Attack);
        }
        else if(DevilName == "BoneMan")
        {
            dataGroup.realTimePlayerData = dataGroup.devilsData[0];
        }
        Debug.Log("ëIù¢" + dataGroup.realTimePlayerData.DevilName);
        Debug.Log("òHúl" + dataGroup.realTimePlayerData.PrefabPath);

        
    }

    /// <summary>
    /// ñÇâ§ÇÃÉåÉxÉãéëóøÇåvéZÇ∑ÇÈ
    /// </summary>
    //public (int , float)  CalculateDevilLevel(int level, float exp)
    //{
    //    int resultlevel = 1;
    //    float ramainEx = 1.2f;
    //    return (resultlevel, ramainEx);
    //}
    //var(level, exp) = CalculateDevilLevel(1, 1.2f);
    //int i = level;

}

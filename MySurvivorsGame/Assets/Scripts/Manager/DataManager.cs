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
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[2].Path);
        excelSheetName = "RealTimeData";
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.realTimeData = excelReadWrite.ParseDataJson<RealTimeData>(excelRowData);
        //------------------------------------------------------------------------------------
        excelPath = Path.Combine(Application.streamingAssetsPath, dataGroup.datasPath[3].Path);
        excelSheetName = "RealTimePlayerData";
        excelRowData = excelReadWrite.ReadExcel(excelPath, excelSheetName);
        dataGroup.realTimePlayerData = excelReadWrite.ParseDataJson<RealTimePlayerData>(excelRowData);
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
        excelReadWrite.WriteExcelData<RealTimePlayerData>(excelPath, excelSheetName, dataGroup.realTimePlayerData);
    }

    // ChooseDevilScene----------------------------------------------------------------------------------------------------------------------
    public void ChooseDevil(string DevilName)
    {
        dataGroup.realTimePlayerData.ChooseDevil = DevilName;        
        Debug.Log("ëIù¢" + dataGroup.realTimePlayerData.ChooseDevil);
    }
}

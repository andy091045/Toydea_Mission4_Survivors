using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataProcess;
using System.IO;
using System;
using System.Runtime.Serialization;

namespace DataProcess
{
    public class DataReadStore
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

    }

    [Serializable]
    public class DataGroup
    {
        public List<DatasPath> datasPath = new List<DatasPath>();
        public RealTimeData realTimeData = new RealTimeData();
        public RealTimePlayerData realTimePlayerData = new RealTimePlayerData();
    }

    [Serializable]
    public class DatasPath
    {
        [field: SerializeField] public string Name { get; set; }
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
        [field: SerializeField] public string ChooseDevil { get; set; }

    }
}



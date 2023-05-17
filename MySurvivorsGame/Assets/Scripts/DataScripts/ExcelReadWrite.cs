using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace DataProcess
{
    public class ExcelReadWrite
    {
        public DataRowCollection ReadExcel(string excelPath, string excelSheet)
        {
            using (FileStream fileStream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

                var result = excelReader.AsDataSet();

                return result.Tables[excelSheet].Rows;
            }
        }

        /*
        public List<DatasPath> ParseDataToJson(DataRowCollection excelRowData)
        {
            List< DatasPath > datasPathList = new List< DatasPath >();
            DatasPath datasPath;
            for (int i = 1; i < excelRowData.Count; i++)
            {
                datasPath = new DatasPath();
                datasPath.Name = excelRowData[i][0].ToString();
                datasPath.Path = excelRowData[i][1].ToString();

                datasPathList.Add(datasPath);
            }

            return datasPathList;
        }
        */
        

        public List<T> ParseListDataJson<T>(DataRowCollection excelData)
        {
            List<T> resultList = new List<T>();

            for (int i = 1; i < excelData.Count; i++)
            {
                T data = Activator.CreateInstance<T>();

                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int j = 0; j < properties.Length; j++)
                {
                    PropertyInfo property = properties[j];

                    for(int u = 0; u < excelData[0].Table.Columns.Count; u++)
                    {
                        if (excelData[0][u].ToString() == property.Name)
                        {
                            var value = excelData[i][u];
                            property.SetValue(data, value);
                        }
                    }
                }
                resultList.Add(data);
            }

            return resultList;
        }

        public T ParseDataJson<T>(DataRowCollection excelData)
        {
            
            T data = Activator.CreateInstance<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int j = 0; j < properties.Length; j++)
            {
                    PropertyInfo property = properties[j];

                    for (int u = 0; u < excelData[0].Table.Columns.Count; u++)
                    {
                        if (excelData[0][u].ToString() == property.Name)
                        {
                            var value = excelData[1][u];                               
                            property.SetValue(data, value);
                        }
                    }                
            }

            return data;
        }
    }

}




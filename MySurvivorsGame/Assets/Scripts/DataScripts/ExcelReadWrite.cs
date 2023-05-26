using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System;
using System.Reflection;
using OfficeOpenXml;

namespace DataProcess
{
    public class ExcelReadWrite
    {
        public DataRowCollection ReadExcel(string excelPath, string excelSheet)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (FileStream fileStream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

                var result = excelReader.AsDataSet();                

                return result.Tables[excelSheet].Rows;
            }
        }

        public List<T> ParseListDataJson<T>(DataRowCollection excelData)
        {
            List<T> resultList = new List<T>();

            // i = 0: data_name
            // i = 1: data_des
            // i = 2: data_type
            for (int i = 3; i < excelData.Count; i++)
            {
                T data = Activator.CreateInstance<T>();

                PropertyInfo[] properties = typeof(T).GetProperties();

                data = ParseData<T>(excelData, i, data, properties);

                resultList.Add(data);
            }

            return resultList;
        }

        public T ParseDataJson<T>(DataRowCollection excelData)
        {

            T data = Activator.CreateInstance<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            // i = 0: data_name
            // i = 1: data_des
            // i = 2: data_type
            data = ParseData<T>(excelData, 3, data, properties);

            return data;
        }

        private T ParseData<T>(DataRowCollection excelData, int initNumber, T data, PropertyInfo[] properties)
        {
            for (int j = 0; j < properties.Length; j++)
            {
                PropertyInfo property = properties[j];

                for (int u = 0; u < excelData[0].Table.Columns.Count; u++)
                {
                    if (excelData[0][u].ToString() == property.Name)
                    {
                        var value = excelData[initNumber][u];
                        if (property.PropertyType == typeof(float))
                        {
                            value = float.Parse(value.ToString());
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            value = int.Parse(value.ToString());
                        }
                        else
                        {
                            value = value.ToString();
                        }
                        property.SetValue(data, value);
                    }
                }
            }
            var result = data;
            return result;
        }
        
        public void WriteExcelData<T>(string excelPath, string excelSheet, T data)
        {
            if (File.Exists(excelPath))
            {
                FileInfo existingFile = new FileInfo(excelPath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[excelSheet];
                    
                    int propertyCount = typeof(T).GetProperties().Length;

                    PropertyInfo[] properties = typeof(T).GetProperties();

                    for (int i = 0; i < propertyCount; i++)
                    {
                        object value = properties[i].GetValue(data); 
                        excelWorksheet.Cells[4, i+1].Value = value;
                    }

                    package.Save();
                }
            }            
        }
    }

}




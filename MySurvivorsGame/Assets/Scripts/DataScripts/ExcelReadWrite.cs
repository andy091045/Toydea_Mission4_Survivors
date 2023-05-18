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

        public List<T> ParseListDataJson<T>(DataRowCollection excelData)
        {
            List<T> resultList = new List<T>();

            // i = 0: data_name
            // i = 1: data_type
            for (int i = 2; i < excelData.Count; i++)
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
                            var value = excelData[i][u];
                            if (property.PropertyType == typeof(float))
                            {
                                value = float.Parse(value.ToString());
                            }
                            else if (property.PropertyType == typeof(int))
                            {
                                value = int.Parse(value.ToString());
                            }
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
                        var value = excelData[2][u];
                        if (property.PropertyType == typeof(float))
                        {
                            value = float.Parse(value.ToString());
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            value = int.Parse(value.ToString());
                        }
                        property.SetValue(data, value);
                    }
                }
            }

            return data;
        }

        //private T ParseData<T>(DataRowCollection excelData, int initNumber, T data) 
        //{

        //    var result = data;
        //    return result;       
        //}
    }

}




using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using DataProcess;
using System.Security.AccessControl;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class ExcelReadWriteTest
{
    [Test]
    [TestCase()]
    public void ReadExcel_Test()
    {
        var excelReadWrite = new ExcelReadWrite();
        string filePath = Path.Combine(Application.streamingAssetsPath, "./TestsData/Test.xlsx"); ;
        string sheetName = "Test1";
        var excelRowData = excelReadWrite.ReadExcel(filePath, sheetName);

        Assert.That(excelRowData[0][0], Is.EqualTo("美味しい"));
        Assert.That(excelRowData[0][1], Is.EqualTo("卡夫卡我媽媽"));
        Assert.That(excelRowData[0][2], Is.EqualTo("1+1等於多少"));
        Assert.That(excelRowData[1][0], Is.EqualTo("int"));
        Assert.That(excelRowData[1][1], Is.EqualTo("string"));
        Assert.That(excelRowData[1][2], Is.EqualTo("float"));
        Assert.That(excelRowData[2][0].GetType, Is.EqualTo(typeof(double)));
        Assert.That(int.Parse(excelRowData[2][0].ToString()), Is.EqualTo(987));
        Assert.That(excelRowData[2][1], Is.EqualTo("孝心變質"));
        Assert.That(float.Parse(excelRowData[2][2].ToString()), Is.EqualTo(3.0f));
    }

    [TestCase()]
    public void ParseDataJson_Test()
    {
        TestData testData = new TestData();

        var excelReadWrite = new ExcelReadWrite();
        string filePath = Path.Combine(Application.streamingAssetsPath, "./TestsData/Test.xlsx"); ;
        string sheetName = "Test2";
        var excelRowData = excelReadWrite.ReadExcel(filePath, sheetName);
        testData = excelReadWrite.ParseDataJson<TestData>(excelRowData);

        Assert.That(testData.Problem1, Is.EqualTo("987"));
        Assert.That(testData.Problem2, Is.EqualTo("孝心變質"));
        Assert.That(testData.ProblemC, Is.EqualTo(3));
    }

    [TestCase()]
    public void ParseListDataJson_Test()
    {
        var TestList = new TestDataList();

        var excelReadWrite = new ExcelReadWrite();
        string filePath = Path.Combine(Application.streamingAssetsPath, "./TestsData/Test.xlsx"); ;
        string sheetName = "Test2";
        var excelRowData = excelReadWrite.ReadExcel(filePath, sheetName);
        TestList.testDataList = excelReadWrite.ParseListDataJson<TestData>(excelRowData);

        Assert.That(TestList.testDataList[0].Problem1, Is.EqualTo("987"));
        Assert.That(TestList.testDataList[0].Problem2, Is.EqualTo("孝心變質"));
        Assert.That(TestList.testDataList[0].ProblemC, Is.EqualTo(3));
    }

    [TestCase()]
    public void DataStoreExcel_Test()
    {
        TestData testData = new TestData();

        var excelReadWrite = new ExcelReadWrite();
        string filePath = Path.Combine(Application.streamingAssetsPath, "./TestsData/Test.xlsx"); ;
        string sheetName = "Test2";
        var excelRowData = excelReadWrite.ReadExcel(filePath, sheetName);
        testData = excelReadWrite.ParseDataJson<TestData>(excelRowData);

        testData.Problem1 = "789";
        excelReadWrite.WriteExcelData(filePath, sheetName, testData);
        excelRowData = excelReadWrite.ReadExcel(filePath, sheetName);
        testData = excelReadWrite.ParseDataJson<TestData>(excelRowData);

        Assert.That(testData.Problem1, Is.EqualTo("789"));

        testData.Problem1 = "987";
        excelReadWrite.WriteExcelData(filePath, sheetName, testData);
        excelRowData = excelReadWrite.ReadExcel(filePath, sheetName);
        testData = excelReadWrite.ParseDataJson<TestData>(excelRowData);

        Assert.That(testData.Problem1, Is.EqualTo("987"));
    }
}

public class TestDataList 
{
    public List<TestData> testDataList = new List<TestData>();
}

public class TestData
{
    public string Problem1 { get; set; }
    public string Problem2 { get; set; }
    public int ProblemC { get; set; }
}

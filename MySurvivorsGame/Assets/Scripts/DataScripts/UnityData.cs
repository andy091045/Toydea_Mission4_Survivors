using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HD.FrameworkDesign;
public class UnityData
{
    public DevilData NowDevilData = new DevilData();  

    public Vector3 PlayerDir = Vector3.zero;
    public Vector3 PreviousPlayerDir = Vector3.zero;
    public Vector3 PlayerPos = Vector3.zero;

    public int VillagersNumber = 0;
    public int WarriorsNumber = 0;

    public List<string> HoldItems = new List<string>();
    public List<string> HoldWeapons = new List<string>();

    public BindableProperty<bool> IsInNirvanaTime = new BindableProperty<bool>();
    public BindableProperty<float> EXP = new BindableProperty<float>();
    public BindableProperty<int> DevilLevel = new BindableProperty<int>();

    public bool IsInNirvana = false;

    public UnityData()
    {
        IsInNirvanaTime.Value = false;
        EXP.Value = 0;
        DevilLevel.Value = 0;
    }
}

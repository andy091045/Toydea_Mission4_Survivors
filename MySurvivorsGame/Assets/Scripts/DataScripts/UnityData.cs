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

    //public List<ItemData> HoldItems = new List<ItemData>();
    //public List<WeaponData> HoldWeapons = new List<string>();

    public BindableProperty<bool> IsInNirvanaTime = new BindableProperty<bool>();
    public BindableProperty<float> EXP = new BindableProperty<float>();
    public BindableProperty<int> DevilLevel = new BindableProperty<int>();

    public UnityData()
    {
        IsInNirvanaTime.Value = false;
        EXP.Value = 0;
        DevilLevel.Value = 0;
    }
}

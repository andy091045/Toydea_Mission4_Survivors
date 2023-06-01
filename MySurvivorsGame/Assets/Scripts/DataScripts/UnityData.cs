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

    public List<ItemsType> HoldItems = new List<ItemsType>();
    public List<WeaponsType> HoldWeapons = new List<WeaponsType>();
}

public enum ItemsType
{
    Speed, AbsorbExpRange
}

public enum WeaponsType
{
    MagicBall
}
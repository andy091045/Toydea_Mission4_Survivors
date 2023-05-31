using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityData
{
    public DevilData NowDevilData = new DevilData();  

    public Vector3 PlayerDir = Vector3.zero;
    public Vector3 PreviousPlayerDir = Vector3.zero;
    public Vector3 PlayerPos = Vector3.zero;

    public int VillagersNumber = 0;
    public int WarriorsNumber = 0;

    public float TotalEXP = 0;
}

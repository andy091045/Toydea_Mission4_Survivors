using ExcelDataReader;
using HD.FrameworkDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class EventManager : MonoBehaviour
{
    //CharacterManager.cs
    public delegate void OccurInstantiateDevilEventHandler();
    public static OccurInstantiateDevilEventHandler OccurInstantiateDevil;

    public delegate void OccurNPCGetHurtEventHandler(string damage, Vector3 worldPos);
    public static OccurNPCGetHurtEventHandler OccurNPCGetHurt;

    public delegate void OccurDevilHPChangeEventHandler(bool isGetHurt);
    public static OccurDevilHPChangeEventHandler OccurDevilHPChange;

    //ChooseItemManager
    public delegate void OccurChooseWeaponEventHandler(string weaponControllerName);
    public static OccurChooseWeaponEventHandler OccurChooseWeapon;

    public delegate void OccurChooseItemEventHandler();
    public static OccurChooseItemEventHandler OccurChooseItem;

    //DevilStats
    public delegate void OccurDevilDeadEventHandler();
    public static OccurDevilDeadEventHandler OccurDevilDead;
}

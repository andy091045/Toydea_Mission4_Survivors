using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //CharacterManager.cs
    public delegate void OccurInstantiateDevilEventHandler();
    public static OccurInstantiateDevilEventHandler OccurInstantiateDevil;

    public delegate void OccurNPCGetHurtEventHandler(string damage, Vector3 worldPos);
    public static OccurNPCGetHurtEventHandler OccurNPCGetHurt;

    public delegate void OccurDevilGetHurtEventHandler();
    public static OccurDevilGetHurtEventHandler OccurDevilGetHurt;

    //EXPBar.cs instance
    public delegate void OccurDevilGetEXPStalEventHandler();
    public static OccurDevilGetEXPStalEventHandler OccurDevilGetEXPStal;
}

using HD.FrameworkDesign;
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

    public static BindableProperty<bool> IsInNirvanaTime = new BindableProperty<bool>();
    public static BindableProperty<float> EXP = new BindableProperty<float>();
    public static BindableProperty<int> DevilLevel = new BindableProperty<int>();

    private void Start()
    {
        KeyInputManager.Instance.onNirvanaUseEvent.AddListener(IsInNirvanaTimeInvoke);
        SetBindablePropertyInitValue();
    }

    void IsInNirvanaTimeInvoke()
    {
        IsInNirvanaTime.Value = true;
    }

    void SetBindablePropertyInitValue()
    {
        IsInNirvanaTime.Value = false;
        EXP.Value = 0;
        DevilLevel.Value = 0;
    }
}

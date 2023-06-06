using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NPCStats : CharacterStats
{
    public NPCsData npcPoolData;

    ObjectPoolGroup objectPoolGroup_;
    float maxLengthWithDevil = 20.0f;

    protected override void Start()
    {
        base.Start();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();     
    }

    protected override void Update()
    {
        base.Update();
        IsLengthWithDevilOver();
    }

    protected virtual void IsLengthWithDevilOver()
    {
        float length = CountLength(transform.position, unityData.PlayerPos);
        if (length > maxLengthWithDevil)
        {
            Dead();
        }
    }

    protected virtual float CountLength(Vector3 pos, Vector3 target)
    {
        float measuredLength = 0;
        measuredLength = Mathf.Pow(target.x - pos.x, 2f) + Mathf.Pow(target.y - pos.y, 2f);
        return Mathf.Sqrt(measuredLength);
    }

    public void SetNPCValue(NPCsData data, int level)
    {
        npcPoolData = data;
        if (level != 0)
        {
            npcPoolData.HP *= level;
            //Debug.LogWarning(npcPoolData.HP);
        }
    }

    protected override void Dead()
    {
        int value = -100;
        for (int i = 0; i < objectPoolGroup_.CrystalPools.Count; i++)
        {
            if (objectPoolGroup_.CrystalPools[i].ObjectName == npcPoolData.DropCrystalType)
            {
                value = i; break;
            }
        }

        if(value == -100)
        {
            Debug.LogError("���Q��������Ipool");
        }

        GameObject crystal = objectPoolGroup_.CrystalPools[value].Pool.GetInstance();
        crystal.transform.position = transform.position;
        crystal.SetActive(true);
        base.Dead();
    }
}

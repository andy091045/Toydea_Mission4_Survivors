using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : CharacterStats
{
    public NPCPoolData npcPoolData;

    float maxLengthWithDevil = 20.0f;

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

    public void SetNPCValue(NPCPoolData data)
    {
        npcPoolData = data;
    }
}

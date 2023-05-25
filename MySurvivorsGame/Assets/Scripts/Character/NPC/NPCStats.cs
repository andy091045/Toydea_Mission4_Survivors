using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : CharacterStats
{
    public NPCPoolData npcPoolData;

    public void SetNPCValue(NPCPoolData data)
    {
        npcPoolData = data;
    }
}

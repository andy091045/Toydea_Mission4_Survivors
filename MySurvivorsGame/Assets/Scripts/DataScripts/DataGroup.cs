using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDefinition
{
    [Serializable]
    public class DataGroup
    {
        public List<DatasPath> datasPath = new List<DatasPath>();

        public List<DevilData> devilsData = new List<DevilData>();
        public DevilData realTimePlayerData = new DevilData();

        public List<WeaponData> weaponsData = new List<WeaponData>();

        public RealTimeData realTimeData = new RealTimeData();

        public List<SceneProcessData> sceneProcessData = new List<SceneProcessData>();

        public List<NPCPoolData> npcPoolsData = new List<NPCPoolData>();
    }

    [Serializable]
    public class DatasPath
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Path { get; set; }
    }

    [Serializable]
    public class RealTimeData
    {
        [field: SerializeField] public float Volume { get; set; }
        [field: SerializeField] public int KillNumber { get; set; }
    }

    [Serializable]
    public class DevilData
    {
        [field: SerializeField] public string DevilName { get; set; }
        [field: SerializeField] public float Attack { get; set; }
        [field: SerializeField] public float HP { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] public float ExpEffect { get; set; }
        [field: SerializeField] public float AbsorbExpRange { get; set; }
        [field: SerializeField] public float DamageCut { get; set; }
        [field: SerializeField] public float Recovery { get; set; }
        [field: SerializeField] public float DropRate { get; set; }
        [field: SerializeField] public string PrefabPath { get; set; }
        [field: SerializeField] public string InitWeapon { get; set; }
    }

    [Serializable]
    public class WeaponData
    {
        [field: SerializeField] public string WeaponName { get; set; }
        [field: SerializeField] public string WeaponPrefabPath { get; set; }
        [field: SerializeField] public string SheetName { get; set; }
        [field: SerializeField] public int NowWeaponLevel { get; set; }
        [field: SerializeField] public List<WeaponLevelData> LevelList { get; set; }
    }

    [Serializable]
    public class WeaponLevelData
    {
        [field: SerializeField] public int Level { get; set; }
        [field: SerializeField] public float Hurt { get; set; }
        [field: SerializeField] public int Number { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] public float Cooldown { get; set; }
        [field: SerializeField] public float Duration { get; set; }

        public WeaponLevelData Clone()
        {
            return new WeaponLevelData()
            {
                Level = Level,
                Hurt = Hurt,
                Number = Number,
                Speed = Speed,
                Cooldown = Cooldown,
                Duration = Duration,
            };            
        }
    }

    [Serializable]
    public class SceneProcessData
    {
        [field: SerializeField] public float Min { get; set; }
        [field: SerializeField] public float Sec { get; set; }
        [field: SerializeField] public int VillagerACount { get; set; }
        [field: SerializeField] public int VillagerBCount { get; set; }
        [field: SerializeField] public int VillagerCCount { get; set; }
        [field: SerializeField] public int SoldierACount { get; set; }
        [field: SerializeField] public int SoldierBCount { get; set; }
        [field: SerializeField] public int RPGKnightCount { get; set; }
        [field: SerializeField] public int RPGMageCount { get; set; }
        [field: SerializeField] public int WarriorCount { get; set; }
    }

    [Serializable]
    public class NPCPoolData
    {
        [field: SerializeField] public string CharacterName { get; set; }
        [field: SerializeField] public int CharacterCount { get; set; }
        [field: SerializeField] public string ObjectPrefabPath { get; set; }
        [field: SerializeField] public string ClassName { get; set; }
        [field: SerializeField] public float HP { get; set; }
        [field: SerializeField] public float Attack { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] public float CooldownDuration { get; set; }
        [field: SerializeField] public string SheetName { get; set; }

        public NPCPoolData Clone()
        {
            return new NPCPoolData()
            {
                CharacterName = CharacterName,
                CharacterCount = CharacterCount,
                ObjectPrefabPath = ObjectPrefabPath,
                ClassName = ClassName,
                HP = HP,
                Attack = Attack,
                Speed = Speed,
                CooldownDuration = CooldownDuration,
                SheetName = SheetName
            };
        }
    }    
}

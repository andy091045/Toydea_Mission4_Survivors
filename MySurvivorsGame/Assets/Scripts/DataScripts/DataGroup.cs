using Codice.CM.Common;
using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using static PlasticPipe.Server.MonitorStats;

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
        public List<NPCsData> npcsData = new List<NPCsData>();
        public List<CrystalsData> crystalsData = new List<CrystalsData>();
        public List<LevelData> levelData = new List<LevelData>();
        public List<ItemData> itemsData = new List<ItemData>();
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
        [field: SerializeField] public float AttackCooldown { get; set; }
        [field: SerializeField] public string PrefabPath { get; set; }
        [field: SerializeField] public string InitWeapon { get; set; }

        public DevilData Clone()
        {
            return new DevilData()
            {
                DevilName = DevilName,
                Attack = Attack,
                HP = HP,
                Speed = Speed,
                ExpEffect = ExpEffect,
                AbsorbExpRange = AbsorbExpRange,
                DamageCut = DamageCut,
                Recovery = Recovery,
                DropRate = DropRate,
                AttackCooldown = AttackCooldown,
                PrefabPath = PrefabPath,
                InitWeapon = InitWeapon                
            };
        }
    }

    [Serializable]
    public class WeaponData
    {
        [field: SerializeField] public string WeaponName { get; set; }
        [field: SerializeField] public string WeaponPrefabPath { get; set; }
        [field: SerializeField] public string SheetName { get; set; }
        [field: SerializeField] public int NowWeaponLevel { get; set; }
        [field: SerializeField] public string UIPath { get; set; }
        [field: SerializeField] public string UIName { get; set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public string ScriptName { get; set; }
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

        public SceneProcessData Clone()
        {
            return new SceneProcessData()
            {
                Min = Min,
                Sec = Sec,
                VillagerACount = VillagerACount,
                VillagerBCount = VillagerBCount,
                VillagerCCount = VillagerCCount,
                SoldierACount = SoldierACount,
                SoldierBCount = SoldierBCount,
                RPGKnightCount = RPGKnightCount,
                RPGMageCount = RPGMageCount,
                WarriorCount = WarriorCount
            };
        }
    }

    [Serializable]
    public class NPCsData
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
        [field: SerializeField] public string DropCrystalType { get; set; }

        public NPCsData Clone()
        {
            return new NPCsData()
            {
                CharacterName = CharacterName,
                CharacterCount = CharacterCount,
                ObjectPrefabPath = ObjectPrefabPath,
                ClassName = ClassName,
                HP = HP,
                Attack = Attack,
                Speed = Speed,
                CooldownDuration = CooldownDuration,
                SheetName = SheetName,
                DropCrystalType = DropCrystalType
            };
        }
    }

    [Serializable]
    public class CrystalsData
    {
        [field: SerializeField] public string CrystalName { get; set; }
        [field: SerializeField] public string PrefabPath { get; set; }        
        [field: SerializeField] public float EXPValue { get; set; }
        [field: SerializeField] public int Count { get; set; }

        public CrystalsData Clone()
        {
            return new CrystalsData()
            {
                CrystalName = CrystalName,
                PrefabPath = PrefabPath,
                EXPValue = EXPValue,
                Count = Count
            };
        }
    }

    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public int Level { get; set; }
        [field: SerializeField] public float NeedEXP { get; set; }

        public LevelData Clone()
        {
            return new LevelData()
            {
                Level = Level,
                NeedEXP = NeedEXP
            };
        }
    }

    [Serializable]
    public class ItemData
    {
        [field: SerializeField] public string ItemName { get; set; }
        [field: SerializeField] public string SheetName { get; set; }
        [field: SerializeField] public int NowItemLevel { get; set; }
        [field: SerializeField] public string UIPath { get; set; }
        [field: SerializeField] public string UIName { get; set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public List<ItemLevelData> LevelList { get; set; }

        public ItemData Clone()
        {
            return new ItemData()
            {
                ItemName = ItemName,
                SheetName = SheetName,
                NowItemLevel = NowItemLevel,
                UIPath = UIPath,
                UIName = UIName,
                Description = Description,
                LevelList = LevelList
            };
        }
    }

    [Serializable]
    public class ItemLevelData
    {
        [field: SerializeField] public int Level { get; set; }
        [field: SerializeField] public float Value { get; set; }

        public ItemLevelData Clone()
        {
            return new ItemLevelData()
            {
                Level = Level,
                Value = Value
            };
        }
    }
}

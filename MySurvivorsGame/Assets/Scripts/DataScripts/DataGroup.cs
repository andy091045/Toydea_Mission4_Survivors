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
        //public List<WeaponData> RealTimeWeaponsData = new List<WeaponData>();

        public RealTimeData realTimeData = new RealTimeData();
        
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

    }
}

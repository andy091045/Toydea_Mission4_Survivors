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
        public RealTimeData realTimeData = new RealTimeData();
        public RealTimePlayerData realTimePlayerData = new RealTimePlayerData();
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
    public class RealTimePlayerData
    {
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] public float Exp { get; set; }
        [field: SerializeField] public float AbsorbExpRange { get; set; }
        [field: SerializeField] public float DamageCut { get; set; }
        [field: SerializeField] public float Recovery { get; set; }
        [field: SerializeField] public float DropRate { get; set; }
        [field: SerializeField] public int SoulNumber { get; set; }
        [field: SerializeField] public string ChooseDevil { get; set; }
        [field: SerializeField] public string PrefabPath { get; set; }

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
    }
}

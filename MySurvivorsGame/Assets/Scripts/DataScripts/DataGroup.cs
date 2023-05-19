using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDefinition
{
    public class DataGroup
    {
        public List<DatasPath> datasPath = new List<DatasPath>();
        public RealTimeData realTimeData = new RealTimeData();
        public RealTimePlayerData realTimePlayerData = new RealTimePlayerData();
    }

    public class DatasPath
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Path { get; set; }
    }

    public class RealTimeData
    {
        [field: SerializeField] public float Volume { get; set; }
        [field: SerializeField] public int KillNumber { get; set; }
    }

    public class RealTimePlayerData
    {
        [field: SerializeField] public float Speed { get; set; }
        [field: SerializeField] public float Exp { get; set; }
        [field: SerializeField] public float AbsorbExpRange { get; set; }
        [field: SerializeField] public float DemageCut { get; set; }
        [field: SerializeField] public float Recovery { get; set; }
        [field: SerializeField] public float DropRate { get; set; }
        [field: SerializeField] public int SoulNumber { get; set; }
        [field: SerializeField] public string ChooseDevil { get; set; }

    }

}

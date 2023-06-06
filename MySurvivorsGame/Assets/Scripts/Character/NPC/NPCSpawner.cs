using System;
using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using Codice.CM.SEIDInfo;
using HD.Pooling;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NPCSpawner : MonoBehaviour
{
    DataManager data_;
    ObjectPoolGroup objectPoolGroup_;
    UnityData unityData_;
    GameObject npcField_;


    List<NPCsData> poolData_ = new List<NPCsData>();

    [SerializeField] private float totalTime_ = 0;
    [SerializeField] private int targetTimeNum_ = 0;
    [SerializeField] private SceneProcessData sceneProcessData_;

    /// <summary>
    /// 生成データのリスト
    /// </summary>
    List<SpawnStrategyInfo> spawnStrategyInfos_ = new();

    private void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        unityData_ = GameContainer.Get<UnityData>();
        npcField_ = GameObject.Find("createNPCField");
        poolData_ = data_.dataGroup.npcsData;
        InitializePools();        
    }

    void Update()
    {
        totalTime_ += Time.deltaTime;
        UpdateSceneProcessData();
        AddPrefabToGame();
    }

    async void InitializePools()
    {
        for (int i = 0; i < poolData_.Count; i++)
        {
            GameObject pool = new GameObject(poolData_[i].CharacterName + "Pool");
            BasicPool basicPool = pool.AddComponent<BasicPool>();
            GameObject prefab = await Addressables.LoadAssetAsync<GameObject>(poolData_[i].ObjectPrefabPath).Task;            
            int count = poolData_[i].CharacterCount;
            basicPool.InstantiatePool(poolData_[i].Clone().CharacterName, prefab, count);
            objectPoolGroup_.AddNPCPool(basicPool);
            pool.transform.parent = transform;
        }
        UpdateSceneProcessData();
        //AddPrefabToGame();
    }       

    void UpdateSceneProcessData()
    {
        float targetTotalTime = data_.dataGroup.sceneProcessData[targetTimeNum_].Min * 60 + data_.dataGroup.sceneProcessData[targetTimeNum_].Sec;
        if (totalTime_ > targetTotalTime)
        {
            sceneProcessData_ = data_.dataGroup.sceneProcessData[targetTimeNum_];
            if (targetTimeNum_ < data_.dataGroup.sceneProcessData.Count - 1)
            {
                targetTimeNum_++;
            }
        }
    }

    //int Count = 0;

    void AddPrefabToGame()
    {
        if (objectPoolGroup_.NPCPools.Count == poolData_.Count)
        {           
            initSpawnInfoList();

            foreach (var info in spawnStrategyInfos_)
            {
                spawnNpc(info);
            }
        }
    }

    void initSpawnInfoList()
    {
        // 村人のデータ
        spawnStrategyInfos_.Add(getSpawnStrategyInfo<VillagerStats>());

        // 勇者のデータ
        spawnStrategyInfos_.Add(getSpawnStrategyInfo<WarriorStats>());
    }

    /// <summary>
    /// Npc生成実処理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void spawnNpc(SpawnStrategyInfo spawnInfo)
    {
        // 作成必要ですか
        while (spawnInfo.NeedSpawn)
        {
            Vector3 pos = npcField_.GetComponent<CreateNPCField>().GetNPCPosition();

            // PoolからInstanceを取る
            var npc = spawnInfo.Pool.GetInstance();

            npc.transform.position = pos;

            // 　Npcにデータを設定する
            spawnInfo.GetComponentFunc.Invoke(npc).SetNPCValue(spawnInfo.PoolData.Clone(), unityData_.DevilLevel.Value);

            npc.SetActive(true);

            // 量を更新する
            var newCount = spawnInfo.GetCurrentNumber.Invoke() + 1;
            spawnInfo.SetCurrentNumber.Invoke(newCount);
        }
    }

    /// <summary>
    /// 生成必要な設定を取る
    /// </summary>
    /// <returns></returns>
    SpawnStrategyInfo getSpawnStrategyInfo<T>() where T : NPCStats
    {
        var info = new SpawnStrategyInfo();
        info.GetComponentFunc = o => o.GetComponent<T>();
        // 村人生成必要なもの
        if (typeof(T) == typeof(VillagerStats))
        {
            info.GetCurrentNumber = () => unityData_.VillagersNumber;
            info.SetCurrentNumber = newNum => unityData_.VillagersNumber = newNum;
            info.GetGoalNumber = () => sceneProcessData_.VillagerACount;
            info.Pool = objectPoolGroup_.NPCPools[0].Pool;
            info.PoolData = poolData_[0];
            return info;
        }

        // 勇者生成必要なもの
        if (typeof(T) == typeof(WarriorStats))
        {
            info.GetCurrentNumber = () => unityData_.WarriorsNumber;
            info.SetCurrentNumber = newNum => unityData_.WarriorsNumber = newNum;
            info.GetGoalNumber = () => sceneProcessData_.WarriorCount;
            info.Pool = objectPoolGroup_.NPCPools[1].Pool;
            info.PoolData = poolData_[1];
            return info;
        }

        throw new UnityException($"Npc作成データを取得エラー: {typeof(T)}");
    }

    /// <summary>
    /// 生成必要な設定
    /// </summary>
    class SpawnStrategyInfo
    {
        /// <summary>
        /// 今の量を取る方法
        /// </summary>
        public Func<int> GetCurrentNumber;

        /// <summary>
        /// 今の量を設定する方法
        /// </summary>
        public Action<int> SetCurrentNumber;

        /// <summary>
        /// 目標の量を取る方法
        /// </summary>
        public Func<int> GetGoalNumber;

        /// <summary>
        /// データ
        /// </summary>
        public NPCsData PoolData;

        /// <summary>
        /// Objectを作る必要なPool
        /// </summary>
        public IPool<GameObject> Pool;

        public Func<GameObject, NPCStats> GetComponentFunc;

        /// <summary>
        /// 作成必要ですか
        /// </summary>
        public bool NeedSpawn => GetCurrentNumber.Invoke() < GetGoalNumber.Invoke();
    }
}

using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NPCSpawner : MonoBehaviour
{
    DataManager data_;
    ObjectPoolGroup objectPoolGroup_;
    UnityData unityData_;    
    GameObject npcField_;
    

    List<NPCPoolData> poolData_ = new List<NPCPoolData>();

    [SerializeField] private float totalTime_ = 0;
    [SerializeField] private int targetTimeNum_ = 0;
    [SerializeField] private SceneProcessData sceneProcessData_;

    private void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        unityData_ = GameContainer.Get<UnityData>();        
        npcField_ = GameObject.Find("createNPCField");
        poolData_ = data_.dataGroup.npcPoolsData;
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
            pool.AddComponent<BasicPool>();
            pool.GetComponent<BasicPool>().Prefab = await Addressables.LoadAssetAsync<GameObject>(poolData_[i].ObjectPrefabPath).Task;
            //System.Type type = System.Type.GetType(poolData_[i].ClassName);
            //pool.GetComponent<BasicPool>().Prefab.AddComponent();
            pool.GetComponent<BasicPool>().Count = poolData_[i].CharacterCount;
            pool.GetComponent<BasicPool>().InstantiateAndAddToGroup();
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
        if (objectPoolGroup_.objectPools_.Count != 0)
        {
            //int maxNumber = 0;
            //for (int i = 0; i < poolData_.Count; i++)
            //{
            //    switch(poolData_[i].CharacterName)
            //    {
            //        case "VillagerA":
            //            maxNumber = sceneProcessData_.VillagerACount;
            //            break;
            //        case "Warrior":
            //            maxNumber = sceneProcessData_.WarriorCount;
            //            break;
            //        default: break;
            //    }

            //    while (unityData_.npcNumber[i] < maxNumber)
            //    {
            //        Vector3 pos = npcField_.GetComponent<CreateNPCField>().GetNPCPosition();
            //        var apple = objectPoolGroup_.objectPools_[0].Pool.GetInstance();
            //        apple.transform.position = pos;
            //        //apple.name = $"{Count++}{apple.name}";
            //        System.Type type = System.Type.GetType(poolData_[i].ClassName);                    
            //        apple.GetComponent(type).SetNPCValue(poolData_[i].Clone());
            //        apple.SetActive(true);
            //        Debug.Log("村民" + unityData_.npcNumber + "的位置在: " + pos + "血量: " + apple.GetComponent<VillagerStats>().npcPoolData.HP);
            //        unityData_.npcNumber[i]++;
            //    }
            //}

            //村民生成
            while (unityData_.VillagersNumber < sceneProcessData_.VillagerACount)
            {
                Vector3 pos = npcField_.GetComponent<CreateNPCField>().GetNPCPosition();
                var npc = objectPoolGroup_.objectPools_[0].Pool.GetInstance();
                npc.transform.position = pos;
                //apple.name = $"{Count++}{apple.name}";
                npc.GetComponent<VillagerStats>().SetNPCValue(poolData_[0].Clone());
                npc.SetActive(true);
                //Debug.Log("村民" + unityData_.VillagersNumber + "的位置在: " + pos + "血量: " + apple.GetComponent<VillagerStats>().npcPoolData.HP);
                unityData_.VillagersNumber++;
            }

            //勇者生成
            while (unityData_.WarriorsNumber < sceneProcessData_.WarriorCount)
            {
                Vector3 pos = npcField_.GetComponent<CreateNPCField>().GetNPCPosition();
                var npc = objectPoolGroup_.objectPools_[1].Pool.GetInstance();
                npc.transform.position = pos;
                //apple.name = $"{Count++}{apple.name}";
                npc.GetComponent<WarriorStats>().SetNPCValue(poolData_[1].Clone());
                npc.SetActive(true);
                unityData_.WarriorsNumber++;
            }
        }       
    }    
}

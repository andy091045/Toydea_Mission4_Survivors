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

    bool isPoolsComplete = false;

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

        if (isPoolsComplete)
        {
            AddPrefabToGame();
        }        
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
        isPoolsComplete = true;
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
        //if (objectPoolGroup_.objectPools_.Count != 0)
        //{
        //    Debug.Log(objectPoolGroup_.objectPools_[0].Count);
        //    var apple = objectPoolGroup_.objectPools_[0].Pool.GetInstance();
        //    apple.GetComponent<VillagerStats>().SetNPCValue(poolData_[0]);
        //    Debug.Log("444444444444444");
        //}
        if (objectPoolGroup_.objectPools_.Count != 0)
        {
            while (unityData_.npcNumber < sceneProcessData_.VillagerACount)
            {                
                Vector3 pos = npcField_.GetComponent<CreateNPCField>().GetNPCPosition();
                var apple = objectPoolGroup_.objectPools_[0].Pool.GetInstance();
                apple.transform.position = pos;
                //apple.name = $"{Count++}{apple.name}";
                apple.GetComponent<VillagerStats>().SetNPCValue(poolData_[0].Clone());
                apple.SetActive(true);
                Debug.Log("村民" + unityData_.npcNumber + "的位置在: " + pos + "血量: " + apple.GetComponent<VillagerStats>().npcPoolData.HP);
                unityData_.npcNumber++;
            }

            while (unityData_.npcBNumber < sceneProcessData_.VillagerBCount)
            {
                Vector3 pos = npcField_.GetComponent<CreateNPCField>().GetNPCPosition();
                var apple = objectPoolGroup_.objectPools_[1].Pool.GetInstance();
                apple.transform.position = pos;
                //apple.name = $"{Count++}{apple.name}";
                apple.GetComponent<WarriorStats>().SetNPCValue(poolData_[1].Clone());
                apple.SetActive(true);                
                unityData_.npcBNumber++;
            }
        }       
    }    
}

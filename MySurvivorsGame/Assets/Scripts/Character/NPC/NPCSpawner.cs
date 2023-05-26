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
    Camera cam_;
    GameObject npcField_;
    [SerializeField] private float npcFieldLength_ = 25.0f;

    List<NPCPoolData> poolData_ = new List<NPCPoolData>();

    [SerializeField] private float totalTime_ = 0;
    [SerializeField] private int targetTimeNum_ = 0;
    [SerializeField] private SceneProcessData sceneProcessData_;

    List<Vector3> cameraPos_ = new List<Vector3>()
    {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0)
    };
    List<Vector3> createNPCField_ = new List<Vector3>()
    {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0)
    };

    private void Start()
    {
        data_ = GameContainer.Get<DataManager>();
        objectPoolGroup_ = GameContainer.Get<ObjectPoolGroup>();
        unityData_ = GameContainer.Get<UnityData>();

        cam_ = FindObjectOfType<Camera>();
        npcField_ = GameObject.Find("createNPCField");

        poolData_ = data_.dataGroup.npcPoolsData;
        InitializePools();        
    }

    void Update()
    {
        totalTime_ += Time.deltaTime;
        UpdateCreateNPCFieldPos();
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
        UpdateCreateNPCFieldPos();
        UpdateSceneProcessData();
        AddPrefabToGame();
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

    void UpdateCreateNPCFieldPos()
    {
        // 取得相機的四個角的螢幕座標
        Vector3 bottomLeft = new Vector3(0, 0, cam_.nearClipPlane);
        Vector3 bottomRight = new Vector3(Screen.width, 0, cam_.nearClipPlane);
        Vector3 topLeft = new Vector3(0, Screen.height, cam_.nearClipPlane);
        Vector3 topRight = new Vector3(Screen.width, Screen.height, cam_.nearClipPlane);

        // 將螢幕座標轉換為世界座標
        Vector3 worldBottomLeft = cam_.ScreenToWorldPoint(bottomLeft);
        Vector3 worldBottomRight = cam_.ScreenToWorldPoint(bottomRight);
        Vector3 worldTopLeft = cam_.ScreenToWorldPoint(topLeft);
        Vector3 worldTopRight = cam_.ScreenToWorldPoint(topRight);

        cameraPos_[0] = worldTopLeft;
        cameraPos_[1] = worldTopRight;
        cameraPos_[2] = worldBottomRight;
        cameraPos_[3] = worldBottomLeft;

        //----------------------------------------------------------------------------------------------------------------------

        SpriteRenderer spriteRenderer = npcField_.GetComponent<SpriteRenderer>();

        // 獲取Sprite的四個角座標
        bottomLeft = npcField_.transform.position - new Vector3(npcFieldLength_ / 2f, npcFieldLength_ / 2f, 0f);
        bottomRight = npcField_.transform.position + new Vector3(npcFieldLength_ / 2f, -npcFieldLength_ / 2f, 0f);
        topLeft = npcField_.transform.position + new Vector3(-npcFieldLength_ / 2f, npcFieldLength_ / 2f, 0f);
        topRight = npcField_.transform.position + new Vector3(npcFieldLength_ / 2f, npcFieldLength_ / 2f, 0f);

        createNPCField_[0] = topLeft;
        createNPCField_[1] = topRight;
        createNPCField_[2] = bottomRight;
        createNPCField_[3] = bottomLeft;
    }

    void AddPrefabToGame()
    {
        //if (objectPoolGroup_.objectPools_.Count != 0)
        //{
        //    Debug.Log(objectPoolGroup_.objectPools_[0].Count);
        //    var apple = objectPoolGroup_.objectPools_[0].Pool.GetInstance();
        //    apple.GetComponent<VillagerStats>().SetNPCValue(poolData_[0]);
        //    Debug.Log("444444444444444");
        //}

        while (unityData_.npcNumber < sceneProcessData_.VillagerACount)
        {
            Vector3 pos = GetNPCPosition();
            Debug.Log(pos);
            var apple = objectPoolGroup_.objectPools_[0].Pool.GetInstance();
            apple.transform.position = pos;
            apple.GetComponent<VillagerStats>().SetNPCValue(poolData_[0]);
            unityData_.npcNumber++;
        }
    }

    Vector3 GetNPCPosition()
    {
        Vector3 pos;
        pos = GetRandomPosition();
        while (IsInCameraPos(pos))
        {
            pos = GetRandomPosition();
        }
        return pos;
    }

    bool IsInCameraPos(Vector3 pos)
    {
        bool isInCameraPos = true;
        if(pos.y > cameraPos_[0].y)
        {
            isInCameraPos = false;
        }else if (pos.y < cameraPos_[2].y)
        {
            isInCameraPos = false;
        }
        else if (pos.x < cameraPos_[0].x)
        {
            isInCameraPos = false;
        }
        else if (pos.x > cameraPos_[2].x)
        {
            isInCameraPos = false;
        }
        return isInCameraPos;
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;

        // 在第一個方塊的範圍內生成隨機座標
        randomPosition = new Vector3
        (
           Random.Range(createNPCField_[0].x, createNPCField_[1].x),
           Random.Range(createNPCField_[0].y, createNPCField_[3].y),
           0
        );
        return randomPosition;
    }
}

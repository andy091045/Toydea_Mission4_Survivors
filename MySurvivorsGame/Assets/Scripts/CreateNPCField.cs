using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNPCField : MonoBehaviour
{
    UnityData unityData_;
    Camera cam_;

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

    bool canFollowDevil_ = false;

    [SerializeField] private float npcFieldLength_ = 25.0f;

    private void Awake()
    {
        EventManager.OccurInstantiateDevil += FollowDevil;
    }

    void Start()
    {        
        unityData_ = GameContainer.Get<UnityData>();
        cam_ = FindObjectOfType<Camera>();
        UpdateCreateNPCFieldPos();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCreateNPCFieldPos();
        if (canFollowDevil_)
        {
            transform.position = unityData_.PlayerPos;
        }
    }

    void FollowDevil()
    {
        canFollowDevil_ = true;
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

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // 獲取Sprite的四個角座標
        bottomLeft = gameObject.transform.position - new Vector3(npcFieldLength_ / 2f, npcFieldLength_ / 2f, 0f);
        bottomRight = gameObject.transform.position + new Vector3(npcFieldLength_ / 2f, -npcFieldLength_ / 2f, 0f);
        topLeft = gameObject.transform.position + new Vector3(-npcFieldLength_ / 2f, npcFieldLength_ / 2f, 0f);
        topRight = gameObject.transform.position + new Vector3(npcFieldLength_ / 2f, npcFieldLength_ / 2f, 0f);

        createNPCField_[0] = topLeft;
        createNPCField_[1] = topRight;
        createNPCField_[2] = bottomRight;
        createNPCField_[3] = bottomLeft;
    }

    public Vector3 GetNPCPosition()
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
        if (pos.y > cameraPos_[0].y)
        {
            isInCameraPos = false;
        }
        else if (pos.y < cameraPos_[2].y)
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

    private void OnDestroy()
    {
        EventManager.OccurInstantiateDevil -= FollowDevil;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public float CameraPosZ = -10.0f;
    [SerializeField] private Transform target_;
    [SerializeField] private float smoothTime_ = 0.3f; // 平滑移動時間
    [SerializeField] private Vector3 velocity_ = Vector3.zero; // 移動速度

    UnityData unityData_;

    void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
    }

    void LateUpdate()
    {

        // 計算相機目標位置
        Vector3 targetPosition = unityData_.PlayerPos;
        targetPosition.z += CameraPosZ;

        // 平滑移動相機
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity_, smoothTime_);

    }
}

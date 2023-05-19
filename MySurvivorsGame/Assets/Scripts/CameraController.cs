using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target_;
    [SerializeField] private float smoothTime_ = 0.3f; // 平滑移動時間
    [SerializeField] private Vector3 velocity_ = Vector3.zero; // 移動速度

    void Start()
    {
        EventManager.OccurInstantiateDevil += TrackTarget;
    }

    void TrackTarget()
    {
        target_ = FindObjectOfType<PlayerTest>().gameObject.transform;
    }

    void LateUpdate()
    {
        if (target_ != null)
        {
            // 計算相機目標位置
            Vector3 targetPosition = new Vector3(target_.position.x, target_.position.y, transform.position.z);

            // 平滑移動相機
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity_, smoothTime_);
        }        
    }

    private void OnDestroy()
    {
        EventManager.OccurInstantiateDevil -= TrackTarget;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target_;
    [SerializeField] private float smoothTime_ = 0.3f; // �����ړ�����
    [SerializeField] private Vector3 velocity_ = Vector3.zero; // �ړ����x

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
            // �v�Z���@�ڕW�ʒu
            Vector3 targetPosition = new Vector3(target_.position.x, target_.position.y, transform.position.z);

            // �����ړ����@
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity_, smoothTime_);
        }        
    }

    private void OnDestroy()
    {
        EventManager.OccurInstantiateDevil -= TrackTarget;
    }
}

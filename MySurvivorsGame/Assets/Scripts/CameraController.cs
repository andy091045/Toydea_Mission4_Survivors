using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public float CameraPosZ = -10.0f;
    [SerializeField] private Transform target_;
    [SerializeField] private float smoothTime_ = 0.3f; // �����ړ�����
    [SerializeField] private Vector3 velocity_ = Vector3.zero; // �ړ����x

    UnityData unityData_;

    void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
    }

    void LateUpdate()
    {

        // �v�Z���@�ڕW�ʒu
        Vector3 targetPosition = unityData_.PlayerPos;
        targetPosition.z += CameraPosZ;

        // �����ړ����@
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity_, smoothTime_);

    }
}

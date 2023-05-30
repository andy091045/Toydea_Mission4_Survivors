using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameMove : MonoBehaviour
{
    Transform target_;
    [SerializeField] private float speed = 5f;

    public void MoveToTartgetPos(Transform target)
    {
        target_ = target;
        StartCoroutine(MoveToTargetCoroutine());
    }

    private IEnumerator MoveToTargetCoroutine()
    {
        while (Vector3.Distance(transform.position, target_.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, target_.position, speed * Time.deltaTime);
            yield return null;
        }

        // 移动完成后的逻辑
        Debug.Log("移动完成");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FrameMove : MonoBehaviour
{
    Transform target_;
    [SerializeField] private float speed = 5f;

    private void Start()
    {
        DataManager data = GameContainer.Get<DataManager>();    

        if(data.dataGroup.realTimePlayerData.DevilName == "Reaper")
        {
            target_ = GameObject.Find("Reaper").transform;
            MoveToTartgetPos(target_);
        }
        else if (data.dataGroup.realTimePlayerData.DevilName == "BoneMan")
        {
            target_ = GameObject.Find("BoneMan").transform;
            MoveToTartgetPos(target_);
        }
    }

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

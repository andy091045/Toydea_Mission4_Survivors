using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNPCField : MonoBehaviour
{
    UnityData unityData_;

    bool canFollowDevil_ = false;

    void Start()
    {
        EventManager.OccurInstantiateDevil += FollowDevil;
        unityData_ = GameContainer.Get<UnityData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFollowDevil_)
        {
            transform.position = unityData_.PlayerPos;
        }
    }

    void FollowDevil()
    {
        canFollowDevil_ = true;
    }
}

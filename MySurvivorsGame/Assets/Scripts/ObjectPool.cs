using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    ObjectPoolGroup objectPoolGroup;
    private void Awake()
    {
        objectPoolGroup =  GameContainer.Get<ObjectPoolGroup>();
    }

    void Start()
    {
        objectPoolGroup.AddPool(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

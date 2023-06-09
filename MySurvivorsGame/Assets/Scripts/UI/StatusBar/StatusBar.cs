using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    public DataManager dataManager;
    public UnityData unityData;
    public GameObject bar;

    protected virtual void Awake()
    {
        bar = gameObject;
    }

    protected virtual void Start()
    {
        dataManager = GameContainer.Get<DataManager>();
        unityData = GameContainer.Get<UnityData>();
    }

    public void SetState(float current, float max)
    {        
        float state = current/max;
        if(state < 0)
        {
            state = 0;
        }
        bar.transform.localScale = new Vector3(state, transform.localScale.y, transform.localScale.z);
    }
}

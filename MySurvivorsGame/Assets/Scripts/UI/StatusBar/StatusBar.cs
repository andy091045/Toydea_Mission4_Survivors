using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    public DataManager dataManager;
    public UnityData unityData;

    [SerializeField] Transform bar;

    protected virtual void Awake()
    {
        bar = transform;
    }

    protected virtual void Start()
    {
        dataManager = GameContainer.Get<DataManager>();
        unityData = GameContainer.Get<UnityData>();
    }

    public void SetState(float current, float max)
    {        
        float state = (float)current;
        state /= (float)max;
        if(state < 0)
        {
            state = 0;
        }
        bar.transform.localScale = new Vector3(state, transform.localScale.y, transform.localScale.z);
    }
}

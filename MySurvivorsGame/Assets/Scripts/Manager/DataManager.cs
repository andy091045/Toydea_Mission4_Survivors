using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    void Start()
    {
        var dataInit = DataContainer.Get<DataInit>();
    }

    public void ChooseDevil(string DevilName)
    {
        Debug.Log("ëIù¢" + DevilName);
    }
}

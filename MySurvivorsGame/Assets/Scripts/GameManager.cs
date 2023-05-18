using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        var dataInit = DataContainer.Get<DataInit>();
        dataInit.SetDataGroup();
    }
}

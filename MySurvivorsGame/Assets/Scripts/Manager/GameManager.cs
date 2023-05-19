using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    DataReadStore dataReadStore;
    private void Awake()
    {
        dataReadStore = DataContainer.Get<DataReadStore>();
        dataReadStore.SetDataGroup();
    }

    public void LoadScene(int i)
    {
        dataReadStore.StoreDataGroup();
        SceneManager.LoadScene(i);
    }
}

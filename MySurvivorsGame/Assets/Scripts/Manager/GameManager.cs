using DataProcess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    DataReadStore dataReadStore_;
    private void Awake()
    {
        dataReadStore_ = DataContainer.Get<DataReadStore>();
        dataReadStore_.SetDataGroup();
    }

    public void LoadScene(int i)
    {
        dataReadStore_.StoreDataGroup();
        SceneManager.LoadScene(i);
    }
}

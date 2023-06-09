using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;

    private void Awake()
    {
        EventManager.OccurDevilDead += OpenGameOverUI;
    }

    void OpenGameOverUI()
    {
        gameOverUI.SetActive(true);    
    }

    private void OnDestroy()
    {
        EventManager.OccurDevilDead -= OpenGameOverUI;
    }
}

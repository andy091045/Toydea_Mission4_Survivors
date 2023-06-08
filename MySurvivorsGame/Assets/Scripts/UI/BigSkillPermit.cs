using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigSkillPermit : MonoBehaviour
{
    public GameObject BigSkillObject;
    Text text_;

    UnityData unityData_;
    Coroutine blinkingCoroutine;
    bool isBlinking = false;

    private void Start()
    {
        unityData_ = GameContainer.Get<UnityData>();
        unityData_.TotalDeadCount.OnValueChanged += StartBlinking;
        text_ = BigSkillObject.GetComponent<Text>();
        StopBlinking();
        BigSkillObject.SetActive(false);
    }

    void StartBlinking(int count)
    {
        if(count >= 100)
        {
            if (!isBlinking)
            {
                // 開始閃爍
                blinkingCoroutine = StartCoroutine(BlinkCoroutine());
                isBlinking = true;
            }
        }
        else
        {
            StopBlinking();
        }        
    }
    private void StopBlinking()
    {
        if (isBlinking)
        {
            // 停止閃爍並隱藏文字
            StopCoroutine(blinkingCoroutine);
            text_.gameObject.SetActive(false);
            isBlinking = false;
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            // 切換狀態
            text_.gameObject.SetActive(!BigSkillObject.activeSelf);

            yield return new WaitForSeconds(0.8f);
        }
    }
}

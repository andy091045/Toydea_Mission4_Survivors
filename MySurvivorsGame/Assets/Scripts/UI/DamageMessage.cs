using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float duration = 0.2f;
    private void Start()
    {
        //Destroy(gameObject, duration);
        StartCoroutine(ScaleDownCoroutine());
    }

    private System.Collections.IEnumerator ScaleDownCoroutine()
    {
        Vector3 initialScale = transform.localScale; // 初始比例
        Vector3 targetScale = Vector3.zero; // 目标比例

        float elapsedTime = 0f; // 已经过的时间

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // 使用Lerp函数计算当前时间点的比例
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null;
        }
        Destroy(gameObject);
    }
}

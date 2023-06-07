using DataDefinition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TMPro;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] GameObject damageMessage_;

    float offsetY_ = 1.0f;
    void Start()
    {
        InstantiatePrefab();
        EventManager.OccurNPCGetHurt += PostMessage;        
    }

    private async void InstantiatePrefab()
    {
        damageMessage_ = await Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/UI/DamageMessage.prefab").Task;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PostMessage(string text, Vector3 worldPos)
    {
        //Vector3 textPos = new Vector3(transform.position.x, transform.position.y + offsetY_, transform.position.z);
        GameObject print = Instantiate(damageMessage_, transform);
        print.transform.position = worldPos;
        print.GetComponent<TextMeshPro>().text = text;
    }

    private void OnDestroy()
    {
        EventManager.OccurNPCGetHurt -= PostMessage;
    }
}

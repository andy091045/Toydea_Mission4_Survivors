using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var scriptA = ContainerModel.Get<ScriptA>();
        Debug.Log("Â_â ù…ó " + scriptA.AppleNum);
        scriptA.AppleNum++;
        Debug.Log("Â_â ù…ó " + scriptA.AppleNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

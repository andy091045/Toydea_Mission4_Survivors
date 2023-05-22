using HD.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KeyInputManager : TSingletonMonoBehavior<KeyInputManager>
{
    [Header("PlayerMoveEvent")]
    public UnityEvent<float> onHorizontalMoveEvent = new UnityEvent<float>();
    public UnityEvent<float> onVerticalMoveEvent = new UnityEvent<float>();    

    private void Update()
    {
        GetKeyInput();
    }

    private void GetKeyInput()
    {
        onHorizontalMoveEvent.Invoke(Input.GetAxisRaw("Horizontal"));
        onVerticalMoveEvent.Invoke(Input.GetAxisRaw("Vertical"));        
    }

    private void OnDestroy()
    {
        onHorizontalMoveEvent.RemoveAllListeners();
        onVerticalMoveEvent.RemoveAllListeners();
    }
}

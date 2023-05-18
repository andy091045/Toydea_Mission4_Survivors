using HD.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KeyInputManager : TSingletonMonoBehavior<KeyInputManager>
{
    [Header("PlayerMoveEvent")]
    public UnityEvent onTowardRightMoveEvent = new UnityEvent();
    public UnityEvent onTowardLeftMoveEvent = new UnityEvent();
    public UnityEvent onTowardUpMoveEvent = new UnityEvent();
    public UnityEvent onTowardDownMoveEvent = new UnityEvent();

    private void Update()
    {
        GetKeyInput();
    }

    private void GetKeyInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            onTowardRightMoveEvent.Invoke();
        }

        if (Input.GetKey(KeyCode.A))
        {
            onTowardLeftMoveEvent.Invoke();
        }

        if (Input.GetKey(KeyCode.W))
        {
            onTowardUpMoveEvent.Invoke();
        }

        if (Input.GetKey(KeyCode.S))
        {
            onTowardDownMoveEvent.Invoke();
        }
    }

    private void OnDestroy()
    {
        onTowardRightMoveEvent.RemoveAllListeners();
        onTowardLeftMoveEvent.RemoveAllListeners();
        onTowardUpMoveEvent.RemoveAllListeners() ;
        onTowardDownMoveEvent.RemoveAllListeners();
    }
}

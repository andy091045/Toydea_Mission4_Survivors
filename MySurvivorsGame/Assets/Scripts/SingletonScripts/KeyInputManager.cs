using HD.FrameworkDesign;
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
    //public UnityEvent onNirvanaUseEvent = new UnityEvent();
    public BindableProperty<bool> IsInNirvanaTime = new BindableProperty<bool>();

    private void Update()
    {
        GetKeyInput();
    }

    private void GetKeyInput()
    {
        onHorizontalMoveEvent.Invoke(Input.GetAxisRaw("Horizontal"));
        onVerticalMoveEvent.Invoke(Input.GetAxisRaw("Vertical"));    

        if(Input.GetKeyDown(KeyCode.Space))
        {
            IsInNirvanaTime.Value = true;
        }
    }

    private void OnDestroy()
    {
        onHorizontalMoveEvent.RemoveAllListeners();
        onVerticalMoveEvent.RemoveAllListeners();
    }
}

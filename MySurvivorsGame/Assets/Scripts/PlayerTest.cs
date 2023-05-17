using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        KeyInputManager.Instance.onTowardRightMoveEvent.AddListener(MoveRight);
        KeyInputManager.Instance.onTowardLeftMoveEvent.AddListener(MoveLeft);
    }

    private void MoveRight()
    {
        transform.position += new Vector3( 2 * Time.deltaTime, 0, 0);
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(-2 * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

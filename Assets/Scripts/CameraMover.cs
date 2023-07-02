using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    public bool moving = false;

    void Update()
    {
        if(!moving)
            HandleInput();
    }

    private void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.position = new Vector3(transform.position.x + (x * speed), transform.position.y, transform.position.z + (z * speed));
    }
}

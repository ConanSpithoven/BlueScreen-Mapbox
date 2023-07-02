using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float speed = 4f;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float fov;

    private bool moving = false;

    private void Update()
    {
        if(moving && Vector3.Distance(cam.transform.position, targetPosition) > 1f)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, speed * Time.deltaTime);
        }
        if(moving && Vector3.Distance(cam.transform.position, targetPosition) <= 1f)
        {
            moving = false;
            cam.GetComponent<CameraMover>().moving = false;
        }
    }

    public void MoveCamera()
    {
        if (!cam.GetComponent<CameraMover>().moving)
        {
            
            cam.GetComponent<Camera>().fieldOfView = fov;
            moving = true;
            cam.GetComponent<CameraMover>().moving = true;
        }
    }
}

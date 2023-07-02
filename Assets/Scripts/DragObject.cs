using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mousePositionOffset;
    private Vector3 mousePos;
    private Vector3 dragPos;

    private Vector3 GetMouseWorldPosition() {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDown()
    {
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        dragPos = GetMouseWorldPosition() + mousePositionOffset;
        dragPos.y = transform.position.y;
        transform.position = dragPos;
        if (Input.GetKeyDown(KeyCode.Backspace)) { Destroy(this.gameObject); }
    }
}

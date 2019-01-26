using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float dragSpeed = 2;
    public Vector3 inertia = Vector3.zero;
    private Vector3 dragOrigin;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            transform.Translate(inertia, Space.World);
            inertia = inertia.magnitude < 0.01? Vector3.zero : inertia *0.9f;
            return;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);
        inertia = (inertia + move) / 2;

        transform.Translate(move, Space.World);
    }


}

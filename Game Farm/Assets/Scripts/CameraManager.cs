using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public float MaxSize;
    public float MinSize;
    Vector3 dir;
    private Vector3 Origin;
    private Vector3 diff;
    private bool Drag = false;

    // Update is called once per frame
    private void Start()
    {
        dir = new Vector3(0, 0, 0);
    }
    void Update()
    {
        dir.x = dir.y = dir.z = 0;

        if (Input.GetKey(KeyCode.W)) dir.y = 0.1f;
        if (Input.GetKey(KeyCode.S)) dir.y = -0.1f;
        if (Input.GetKey(KeyCode.A)) dir.x = -0.1f;
        if (Input.GetKey(KeyCode.D)) dir.x = 0.1f;
        transform.position += dir;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (Camera.main.orthographicSize > MinSize)
                Camera.main.orthographicSize -= 0.2f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (Camera.main.orthographicSize < MaxSize)
                Camera.main.orthographicSize += 0.2f;
        }
    }
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            diff = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Camera.main.transform.position = Origin - diff;
        }
    }
  
}

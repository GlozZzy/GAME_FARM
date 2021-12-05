using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraManager : MonoBehaviour
{
    public float MaxSize;
    public float MinSize;
    Vector3 dir;

    // Update is called once per frame
    private void Start()
    {
        dir = new Vector3(0, 0, 0);
    }
    void Update()
    {
        dir.x = dir.y = dir.z = 0;

        if (Input.GetKey(KeyCode.W)) dir.y = 0.03f;
        if (Input.GetKey(KeyCode.S)) dir.y = -0.03f;
        if (Input.GetKey(KeyCode.A)) dir.x = -0.03f;
        if (Input.GetKey(KeyCode.D)) dir.x = 0.03f;

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
}

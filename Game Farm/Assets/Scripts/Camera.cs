using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(0,0,0);
        bool bW = (Input.GetKey(KeyCode.W) ? true : false);
        bool bS = (Input.GetKey(KeyCode.S) ? true : false);
        bool bA = (Input.GetKey(KeyCode.A) ? true : false);
        bool bD = (Input.GetKey(KeyCode.D) ? true : false);

        if (bW)
        {
            dir.y = 0.03f;
        }
        if (bS)
        {
            dir.y = -0.03f;
        }
        if (bA)
        {
            dir.x = -0.03f;
        }
        if (bD)
        {
            dir.x = 0.03f;
        }

  

        transform.position += dir;
    }
}

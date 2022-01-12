using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildData
{
    public float px, py, pz;
    public float timer;

    public BuildData(ProductionBuilding build)
    {
        px = build.transform.position.x;
        py = build.transform.position.y;
        pz = build.transform.position.z;
        timer = build.timer;
    }
}

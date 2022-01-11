using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PaddockData
{
    public float px, py, pz;
    public float timer;
    public int numOfProducts;
    public int numOfAnimals;
    public float hungry_timer;

    public PaddockData(Paddock paddock)
    {
        px = paddock.transform.position.x;
        py = paddock.transform.position.y;
        pz = paddock.transform.position.z;
        timer = paddock.timer;
        numOfProducts = paddock.numOfProducts;
        numOfAnimals = paddock.numOfAnimals;
        hungry_timer = paddock.hungry_timer;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour
{
    System.Random random = new System.Random();

    public double NextDouble()
    {
        return random.NextDouble();
    }
}

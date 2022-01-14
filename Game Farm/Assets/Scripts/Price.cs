using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Price : MonoBehaviour
{
    
    double sig1 =0.0005;
    double mu1 = 0;
    Random rnd;

    private void Start()
    {
        rnd = FindObjectOfType<Random>();
    }

    public double GetPar(double w0)
    {
        
        for (int i = 0; i < 100; i++)
        {

            double r = Math.Sqrt(-2.0 * Math.Log(rnd.NextDouble())) * Math.Sin(2.0 * Math.PI * rnd.NextDouble());
            w0 += r;


        }
        return w0;
    }
    public double GetNewP(double rate, double w )
    {
        return Math.Round(rate * Math.Exp((mu1 - sig1 * sig1 / 2) + sig1 * (w)), 2);
    }
}

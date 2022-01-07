using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;
    public double buy_price;
    public double sell_price;
    [System.NonSerialized]
    public Collider2D colaider;
    Price price;
    double w0=0;
    int iter = 0;



    private void Start()
    {
        price = gameObject.GetComponent<Price>();
        colaider = gameObject.GetComponent<Collider2D>();
    }
    public void Update()
    {
        if (iter % 100 == 0)
        {
            if (iter >= 500)
            {
                w0 = 0;
                iter = 0;
            }
            w0 = price.GetPar(w0);
            
            buy_price = price.GetNewP(buy_price, w0);
            
            sell_price = 0.97 * buy_price;
            
        }
        iter++;
    }


    public void SetTransparent(bool available)
    {
        if (available)
        {
            MainRenderer.material.color = Color.green;
        }
        else
        {
            MainRenderer.material.color = Color.red;
        }
    }

    public void SetNormal()
    {
        MainRenderer.material.color = Color.white;
    }
}

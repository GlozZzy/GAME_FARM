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
    float timer = 5;
    public int lvl;



    private void Start()
    {
        MainRenderer = GetComponent<Renderer>();
        price = gameObject.GetComponent<Price>();
        colaider = gameObject.GetComponent<Collider2D>();
        sell_price = 0.9 * buy_price;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0) return;
        {
            timer = 5;
            iter++;
            if (iter % 5 == 0)
            {
                w0 = 0;
                iter = 0;
            }
            w0 = price.GetPar(w0);
            
            buy_price = price.GetNewP(buy_price, w0);
            
            sell_price = 0.9 * buy_price;

        }
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

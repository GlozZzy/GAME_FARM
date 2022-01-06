using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInfo : MonoBehaviour
{
    public string Pname;
    public double buy_price;
    public double sell_price;
    public int exp;
    public int count;
    Price price;
    double w0 = 0;
    int iter = 0;

    private void Start()
    {
        price = gameObject.GetComponent<Price>();
        
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
            Debug.Log(w0);
            buy_price = price.GetNewP(buy_price, w0);
            Debug.Log(buy_price);
            sell_price = 0.97 * buy_price;
            Debug.Log(iter);
        }
        iter++;
    }
}

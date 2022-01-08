using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;
    public int buy_price;
    public int sell_price;
    [System.NonSerialized]
    public Collider2D colaider;


    private void Start()
    {
        colaider = gameObject.GetComponent<Collider2D>();
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

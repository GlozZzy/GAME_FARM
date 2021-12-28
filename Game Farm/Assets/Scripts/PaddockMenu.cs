using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddockMenu : MonoBehaviour
{
    // Start is called before the first frame update
    Paddock paddock;
    Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu()
    {
        canvas.enabled = false;
    }
    public void Open(Paddock p)
    {
        if (paddock) paddock.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        canvas.enabled = true;
        paddock = p;
    }
}

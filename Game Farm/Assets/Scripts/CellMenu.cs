using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMenu : MonoBehaviour
{
    Canvas canvas;
    Field field;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(Field f)
    {
        canvas.enabled = true;
        field = f;
    }

    public void Plant()
    {
        field.Plant();
        Close();
    }

    public void Close()
    {
        canvas.enabled = false;
    }
}

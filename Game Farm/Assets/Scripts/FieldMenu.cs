using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMenu : MonoBehaviour
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
        if (field) field.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }

    public void Open(Field f=null)
    {
        if (field)
            field.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        canvas.enabled = true;
        if (f)
            field = f;
    }

    public void Plant(string str)
    {
        field.Plant(str);
        Close();
    }

    public void Enlarge()
    {
        field.Enlarge();
        Close();
    }

    public void Close()
    {
        if (field)
        {
            field.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            field = null;
        }
        canvas.enabled = false;
        
    }
    public void SellTheField()
    {
        field.SellTheField();
        Close();
    }
}

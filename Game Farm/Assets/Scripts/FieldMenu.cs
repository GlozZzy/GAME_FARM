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

    public void Enlarge()
    {
        field.Enlarge();
        Close();
    }

    public void Close()
    {
        canvas.enabled = false;
        field = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldData
{
    public int stage;
    public float timer;
    public bool isPlanted;
    public bool isBlocked;
    public bool isAlien;
    public string nameProduct;

    public float px, py, pz;

    public FieldData(Field field)
    {
        stage = field.plantStage;
        timer = field.timer;
        isPlanted = field.isPlanted;
        isBlocked = field.isBlocked;
        isAlien = field.isAlien;
        if (field.product) nameProduct = field.product.pname;

        px = field.transform.position.x;
        py = field.transform.position.y;
        pz = field.transform.position.z;
    }
}

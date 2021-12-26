using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldData
{
    public int stage;
    public SpriteRenderer plant;

    public FieldData(Field field)
    {
        stage = field.plantStage;
        plant = field.plant;
    }
}

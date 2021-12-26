using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldData
{
    public int stage;
    public float timer;
    public bool isActive;
    public bool isPlanted;

    public FieldData(Field field)
    {
        stage = field.plantStage;
        timer = field.timer;
        isActive = field.isActive();
        isPlanted = field.isPlanted;

    }
}

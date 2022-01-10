using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WareHouseData
{
    public int maxspace;
    //public int curspace;
    public int[] counts;//

    public WareHouseData(WareHouse warehouse)
    {
        maxspace = warehouse.maxspace;
        //curspace = warehouse.curspace;
        counts = new int[warehouse.counts.Length];
        for (int i=0; i<warehouse.counts.Length; i++)
        {
            counts[i] = warehouse.counts[i];
        }
    }
}


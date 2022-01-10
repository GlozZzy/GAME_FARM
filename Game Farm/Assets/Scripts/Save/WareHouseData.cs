using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WareHouseData
{
    public int maxspace;
    public int[] counts;
    public double[] buy_prices;
    public double[] sell_prices;

    public WareHouseData(WareHouse warehouse)
    {
        maxspace = warehouse.maxspace;
        counts = new int[warehouse.counts.Length];
        buy_prices = new double[warehouse.counts.Length];
        sell_prices = new double[warehouse.counts.Length];

        for (int i=0; i<warehouse.counts.Length; i++)
        {
            counts[i] = warehouse.counts[i];
            buy_prices[i] = warehouse.buy_prices[i];
            sell_prices[i] = warehouse.sell_prices[i];
        }
    }
}


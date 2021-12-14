using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int exp = 0;
    public int lvl = 100;

    public PlayerData(Player player)
    {
        money = player.money;
        exp = player.exp;
        lvl = player.lvl;
    }
}
   
    




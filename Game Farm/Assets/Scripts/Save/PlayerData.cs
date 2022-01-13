using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double money;
    public int exp;
    public int lvltemp;
    public int lvl;

    public PlayerData(Player player)
    {
        money = player.money;
        exp = player.exp;
        lvl = player.lvl;
        lvltemp = player.lvltemp;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int money;
    public int exp = 0;
    public int lvl = 100;
    public Text MoneyTxt;
    public Text ExpTxt;


    // Start is called before the first frame update
    void FixedUpdate()
    {
        MoneyTxt.text = $"{money}";
        ExpTxt.text = $"{exp} / {lvl}";
    }

    // Update is called once per frame
    public bool Transaction(int value)
    {
        if (money + value >= 0)
        {
            money += value;
            return true;
        }
        else
        {
            print("NotEnoughMoney");
            return false;
        }
    }

    public void GetExp(int value)
    {
        exp += value;
        if (exp >= lvl)
            lvlup();
    }


    public void lvlup()
    {
        exp = exp % lvl;
        lvl = (int)(lvl * 1.2);
        print("LevelUp");
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            money = data.money;
            exp = data.exp;
            lvl = data.lvl;
        }
    }

    public void ResetPlayer()
    {
        SaveSystem.ResetData();
    }

}

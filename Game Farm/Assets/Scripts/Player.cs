using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    public double money;
    public int exp = 0;
    public int lvl = 100;
    public Text MoneyTxt;
    public Text ExpTxt;

    FieldMenu notEnougthMoney;

    private void Start()
    {
        notEnougthMoney = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        MoneyTxt.text = $"{Math.Round(money, 2)}";
        ExpTxt.text = $"{exp} / {lvl}";
    }

    // Update is called once per frame
    public bool Transaction(double value)
    {
        if (money + value >= 0)
        {
            money += value;
            return true;
        }
        else
        {
            notEnougthMoney.Open();
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
        SaveSystemPlayer.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystemPlayer.LoadPlayer();

        if (data != null)
        {
            money = data.money;
            exp = data.exp;
            lvl = data.lvl;
        }
    }

    public void ResetPlayer()
    {
        SaveSystemPlayer.ResetData();
    }

}

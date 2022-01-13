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
    public int lvltemp = 0;
    public Text MoneyTxt;
    public Text ExpTxt;
    public Text LVLTxt;
    public Text LvlReq;
    FieldMenu notEnougthMoney;
    public GameObject notEnougthlvl;

    private void Start()
    {
        notEnougthMoney = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
        
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        MoneyTxt.text = $"{Math.Round(money, 2)}";
        ExpTxt.text = $"{exp} / {lvl}";
        LVLTxt.text = $"{lvltemp}";
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
        lvltemp++;
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
            lvltemp = data.lvltemp;
        }
    }

    public void ResetPlayer()
    {
        SaveSystemPlayer.ResetData();
    }
    public bool LvlRecuired(int lvlr)
    {
        if(lvltemp<lvlr)
        {
            Debug.Log("NEKEZ");
            notEnougthlvl.SetActive(true);
            LvlReq.text = "Lvl Required:" + lvlr;
            return false;
        }
        return true;
    }

    public void CloseLvl()
    {
        notEnougthlvl.SetActive(false);
    }

}

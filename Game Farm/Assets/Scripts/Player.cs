using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int money;
    int exp = 0;
    int lvl = 100;
    public Text MoneyTxt;
    public Text ExpTxt;


    // Start is called before the first frame update
    void FixedUpdate()
    {
        MoneyTxt.text = $"{money}";
        ExpTxt.text = $"{exp} / {lvl}";
    }

    // Update is called once per frame
    public void Transaction(int value)
    {
        money += value;
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
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int money = 100;
    public Text moneyTxt;
    // Start is called before the first frame update
    void Start()
    {
        moneyTxt.text = "$" + money;
    }

    // Update is called once per frame
    public void Transaction(int value)
    {
        money += value;
        moneyTxt.text = "$" + money;
    }
    
}

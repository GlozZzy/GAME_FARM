using UnityEngine;

[System.Serializable]
public class Product : MonoBehaviour
{
    public int buy_price;
    public int sell_price;
    public int exp;

    //Plant;s Fieilds
    public Sprite[] plantStages;
    public float[] phases;
    public string pname;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Shop : MonoBehaviour, IPointerClickHandler
{
    public Player player;
    public Canvas CanvasMenu;
    public Canvas buil;
    public Canvas anim;
    public Canvas plan;
    public Canvas prod;
    public Canvas NEM;
    public Canvas BuyMenu;
    public Text canprice;
    ProductInfo product;
    Text BakPrice;
    Text CowPrice;
    Text ChikPrice;
    Building Bakery;
    Building Cow;
    Building Chik;
    public BuildingsGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        buil.enabled = false;
        anim.enabled = false;
        plan.enabled = false;
        prod.enabled = false;
        NEM.enabled = false;
        
        BuyMenu.enabled = false;
        BakPrice = GameObject.FindGameObjectWithTag("BakeryPrice").GetComponent<Text>();
        CowPrice = GameObject.FindGameObjectWithTag("CowPrice").GetComponent<Text>();
        ChikPrice = GameObject.FindGameObjectWithTag("ChikPrice").GetComponent<Text>();
        Bakery = GameObject.FindGameObjectWithTag("Bakery").GetComponent<Building>();
        Cow = GameObject.FindGameObjectWithTag("Cow").GetComponent<Building>();
        Chik = GameObject.FindGameObjectWithTag("Chik").GetComponent<Building>();

    }

    // Update is called once per frame
    void Update()
    {
        BakPrice.text = "Price:" + (Bakery.buy_price);
        CowPrice.text = "Price:" + (Cow.buy_price);
        ChikPrice.text = "Price:" + (Cow.buy_price);
    }


    public void OpenMenu()
    {
        CanvasMenu.enabled = true;
    }

    public void CloseMenu()
    {
        CanvasMenu.enabled = false;
        buil.enabled = false;
        anim.enabled = false;
        plan.enabled = false;
        prod.enabled = false;
        NEM.enabled = false;

    }

    public void BuildingsMenu()
    {
        buil.enabled = true;
        anim.enabled = false;
        plan.enabled = false;
        prod.enabled = false;
        NEM.enabled = false;
        BakPrice.text = "Price:" + (Bakery.buy_price);

    }
    public void ProductsMenu()
    {
        buil.enabled = false;
        anim.enabled = false;
        plan.enabled = false;
        prod.enabled = true;
        NEM.enabled = false;
    }
    public void PlantsMenu()
    {
        buil.enabled = false;
        anim.enabled = false;
        plan.enabled = true;
        prod.enabled = false;
        NEM.enabled = false;
    }
    public void AnimalsMenu()
    {
        buil.enabled = false;
        anim.enabled = true;
        plan.enabled = false;
        prod.enabled = false;
        NEM.enabled = false;
        ChikPrice.text = "Price:" + (Chik.buy_price);
        CowPrice.text = "Price:" + (Cow.buy_price);
    }
    public void BuyCan(ProductInfo produc)
    {
        product = produc;
        canprice.text = "Price:" + product.buy_price;
        BuyMenu.enabled = true;

    }

    public void Not()
    {
        NEM.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (MenuManager.GameIsPaused) return;
        CanvasMenu.gameObject.SetActive(true);
        OpenMenu();
    }
    public void BuyProd()
    {
        int num = int.Parse(BuyMenu.GetComponentInChildren<InputField>().text);
        
        
        WareHouse ware = GameObject.FindGameObjectWithTag("WareHouse").GetComponent<WareHouse>();
        ware.BuyProducts(product, num);
        BuyMenu.enabled = false;
        BuyMenu.GetComponentInChildren<InputField>().text = "";
    }
    public void closeMenu()
    {
        BuyMenu.GetComponentInChildren<InputField>().text = "";
        BuyMenu.enabled = false;
    }
}

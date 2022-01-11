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
    public Text BakPrice;
    public Text CowPrice;
    public Text ChikPrice;
    public Building Bakery;
    public Building Cow;
    public Building Chik;
    public BuildingsGrid grid;
    // Start is called before the first frame update
    public void Start()
    {
        buil.enabled = false;
        anim.enabled = false;
        plan.enabled = false;
        prod.enabled = false;
        NEM.enabled = false;
        
        BuyMenu.enabled = false;
       

    }

    // Update is called once per frame
    public void Update()
    {
        BakPrice.text = "Price:" + (Bakery.buy_price);
        CowPrice.text = "Price:" + (Cow.buy_price);
        ChikPrice.text = "Price:" + (Chik.buy_price);
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
        Debug.Log(Cow.buy_price);
        buil.enabled = false;
        anim.enabled = true;
        plan.enabled = false;
        prod.enabled = false;
        NEM.enabled = false;
        
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

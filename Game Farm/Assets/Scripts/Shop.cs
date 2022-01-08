using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Player player;
    public Canvas CanvasMenu;
    public Canvas buil;
    public Canvas anim;
    public Canvas plan;
    public Canvas prod;
    public Canvas NEM;
    public Text BakPrice;
    public Text CowPrice;
    public Text ChikPrice;
    public Building Bakery;
    public Building Cow;
    public Building Chik;
    public BuildingsGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        buil.enabled = false;
        anim.enabled = false;
        plan.enabled = false;
        prod.enabled = false;
        NEM.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        BakPrice.text = "Price:" + (Bakery.buy_price);
        CowPrice.text = "Price:" + (Cow.buy_price);
    }

    private void OnMouseDown()
    {
        if (MenuManager.GameIsPaused) return;
        CanvasMenu.gameObject.SetActive(true);
        OpenMenu();
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

    public void Not()
    {
        NEM.enabled = true;
    }
    
   
   
}

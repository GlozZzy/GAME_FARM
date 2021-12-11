using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WareHouse : MonoBehaviour
{
    public Player player;
    public Canvas CanvasMenu;
    public Text spaceText;
    public int maxspace;

    private int curspace;
    private List<Product> products;
    

    // Start is called before the first frame update
    void Start()
    {
        curspace = 0;
        products = new List<Product>();
        spaceText.text = "Space: " + curspace + "/" + maxspace;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (MenuManager.GameIsPaused) return;
        OpenMenu();
    }

    public void OpenMenu()
    {
        CanvasMenu.enabled = true;
    }

    public void CloseMenu()
    {
        CanvasMenu.enabled = false;
    }

    public void Expand()
    {
        if (player.Transaction(-100))
        {
            maxspace += 10;
            spaceText.text = "Space: " + curspace + "/" + maxspace;
        }
        else print("NotEnougt money");
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
        curspace++;
    }

    public void SellProduct(Text product)
    {
        int ind = -1;
        for (int i = 0; i < products.Count; i++)
            if (products[i].name == product.text)
                ind = i;
        if (ind >= 0) 
        {
            player.Transaction(products[ind].price);
            curspace--;
            products.RemoveAt(ind);
        }
    }
}

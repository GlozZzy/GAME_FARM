using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WareHouse : MonoBehaviour, IPointerClickHandler
{
    public Player player;
    public Canvas CanvasMenu;
    public Text spaceText;
    public int maxspace;
    public Canvas NEM;
    private int curspace;
    private List<Product> products;


    // Start is called before the first frame update
    void Start()
    {
        curspace = 0;
        products = new List<Product>();
    }

    // Update is called once per frame
    void Update()
    {
        spaceText.text = "Space: " + curspace + "/" + maxspace;
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
        }
        else print("NotEnougt money");
    }

    public bool AddProduct(Product product)
    {
        if (curspace < maxspace)
        {
            
            GameObject obj = GameObject.FindGameObjectWithTag(product.pname);
            ProductInfo inf = obj.GetComponent<ProductInfo>();
            inf.count++;
            string a = "Warehouse" + product.pname;
            GameObject obj1 = GameObject.FindGameObjectWithTag(a);
            var textcount = obj1.transform.Find("count");
            textcount.gameObject.GetComponent<Text>().text = "count: " + (inf.count);

            curspace++;
            return true;
        }
        else return false;
    }

    public void SellProduct(Text product)
    {
        
        GameObject obj = GameObject.FindGameObjectWithTag(product.text);
        ProductInfo inf = obj.GetComponent<ProductInfo>();
        inf.count--;
        string a = "Warehouse" + product.text;
        GameObject obj1 = GameObject.FindGameObjectWithTag(a);
        var textcount = obj1.transform.Find("count");
        textcount.gameObject.GetComponent<Text>().text = "count: " + (inf.count);
        player.money += inf.sell_price;
        curspace--;
        
    }
    public void BuyProducts(ProductInfo inf)
    {
        if (inf.buy_price <= player.money)
        {
            inf.count++;
            string a = "Warehouse" + inf.name;
            GameObject obj1 = GameObject.FindGameObjectWithTag(a);
            var textcount = obj1.transform.Find("count");
            textcount.gameObject.GetComponent<Text>().text = "count: " + (inf.count);

            player.money -= inf.buy_price;
            curspace++;
        }
        else
            NEM.enabled = true;
    }

    public bool TakeProduct(string productName, int count)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(productName);
        ProductInfo inf = obj.GetComponent<ProductInfo>();
        if (inf.count >= count) {
            inf.count-= count;
            string a = "Warehouse" + productName;
            GameObject obj1 = GameObject.FindGameObjectWithTag(a);
            var textcount = obj1.transform.Find("count");
            textcount.gameObject.GetComponent<Text>().text = "count: " + (inf.count);
            player.money += inf.sell_price;
            curspace -= count;
            return true;
        }

        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (MenuManager.GameIsPaused) return;
        OpenMenu();
    }
}

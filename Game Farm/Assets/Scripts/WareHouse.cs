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
    FieldMenu notEnougthSpace;
    FieldMenu notEnougthProducts;


    // Start is called before the first frame update
    void Start()
    {
        curspace = 0;
        products = new List<Product>();
        notEnougthSpace = GameObject.Find("NotEnoughSpace").GetComponent<FieldMenu>();
        notEnougthProducts = GameObject.Find("NotEnoughProducts").GetComponent<FieldMenu>();
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
        else 
        {
            notEnougthSpace.Open();
            return false;
        }
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

    public void BuyProducts(ProductInfo inf, int number)
    {
        if (curspace + number <= maxspace)
        {
            if (player.Transaction(inf.buy_price * number))
            {
                inf.count += number;
                string a = "Warehouse" + inf.name;
                GameObject obj1 = GameObject.FindGameObjectWithTag(a);
                var textcount = obj1.transform.Find("count");
                textcount.gameObject.GetComponent<Text>().text = "count: " + (inf.count);

                curspace += number;
            }
        }
        else
        {
            notEnougthSpace.Open();
        }
    }

    public bool TakeProduct(string productName, int count)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(productName);
        ProductInfo inf = obj.GetComponent<ProductInfo>();
        if (inf.count >= count)
        {
            inf.count -= count;
            string a = "Warehouse" + productName;
            GameObject obj1 = GameObject.FindGameObjectWithTag(a);
            var textcount = obj1.transform.Find("count");
            textcount.gameObject.GetComponent<Text>().text = "count: " + (inf.count);
            player.money += inf.sell_price;
            curspace -= count;
            return true;
        }
        else
        {
            notEnougthProducts.Open();
            return false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (MenuManager.GameIsPaused) return;
        OpenMenu();
    }
}

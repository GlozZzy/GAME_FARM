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
    public int curspace;
    private List<Product> products;
    public List<ProductInfo> listOfObj;
    public int[] counts;
    public double[] buy_prices;
    public double[] sell_prices;
    GameObject obj;
    FieldMenu notEnougthSpace;
    FieldMenu notEnougthProducts;
    public Text carrotCellPrice;
    public Text wheatCellPrice;
    public Text eggCellPrice;
    public Text milkCellPrice;
    public Text meatCellPrice;
    public Text cheeseCellPrice;
    public Text carrotcakeCellPrice;



    // Start is called before the first frame update
    void Start()
    {
        curspace = 0;
        products = new List<Product>();
        notEnougthSpace = GameObject.Find("NotEnoughSpace").GetComponent<FieldMenu>();
        notEnougthProducts = GameObject.Find("NotEnoughProducts").GetComponent<FieldMenu>();
        obj = GameObject.FindGameObjectWithTag("obj");
        counts = new int[obj.transform.childCount-3];   //��� �� ���������
        buy_prices = new double[obj.transform.childCount - 3];
        sell_prices = new double[obj.transform.childCount - 3];
    }

    // Update is called once per frame
    void Update()
    {
        spaceText.text = "Space: " + curspace + "/" + maxspace;
        carrotCellPrice.text = GameObject.FindGameObjectWithTag("Carrot").GetComponent<ProductInfo>().sell_price + "";
        wheatCellPrice.text = GameObject.FindGameObjectWithTag("Wheat").GetComponent<ProductInfo>().sell_price + "";
        milkCellPrice.text = GameObject.FindGameObjectWithTag("Milk").GetComponent<ProductInfo>().sell_price + "";
        eggCellPrice.text = GameObject.FindGameObjectWithTag("Egg").GetComponent<ProductInfo>().sell_price + "";
        meatCellPrice.text = GameObject.FindGameObjectWithTag("Meat").GetComponent<ProductInfo>().sell_price + "";
        cheeseCellPrice.text = GameObject.FindGameObjectWithTag("Cheese").GetComponent<ProductInfo>().sell_price + "";
        carrotcakeCellPrice.text = GameObject.FindGameObjectWithTag("CarrotCake").GetComponent<ProductInfo>().sell_price + "";
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
        print(obj);
        ProductInfo inf = obj.GetComponent<ProductInfo>();
        if (inf.count > 0)
        {
            inf.count--;
            string a = "Warehouse" + product.text;
            GameObject obj1 = GameObject.FindGameObjectWithTag(a);
            var textcount = obj1.transform.Find("count");
            textcount.gameObject.GetComponent<Text>().text = "count: " + (inf.count);
            player.money += inf.sell_price;
            curspace--;
        }
        else
        {
            notEnougthProducts.Open();
        }
    }

    public void BuyProducts(ProductInfo inf, int number)
    {
        if (curspace + number <= maxspace)
        {
            if (player.Transaction(-inf.buy_price * number))
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

    public void SaveWareHouse()
    {
        for (int i = 0; i < counts.Length; i++)
        {
            counts[i] = obj.transform.GetChild(i).GetComponent<ProductInfo>().count;
            buy_prices[i] = obj.transform.GetChild(i).GetComponent<ProductInfo>().buy_price;
            sell_prices[i] = obj.transform.GetChild(i).GetComponent<ProductInfo>().sell_price;
        }
        SaveSystemWareHouse.SaveWareHouse(this);
    }
    public void LoadWareHouse()
    {
        WareHouseData data = SaveSystemWareHouse.LoadWareHouse();
        string[] names = { "Wheat", "Cheese", "CarrotCake", "Milk", "Carrot", "Meat", "Egg" };

        if (data != null)
        {
            maxspace = data.maxspace;
           

            for (int i=0; i<counts.Length; i++)
            {
                var products = new Product();

                products.pname = names[i];
                counts[i] = data.counts[i];
                buy_prices[i] = data.buy_prices[i];
                sell_prices[i] = data.sell_prices[i];

                for (int j=0; j<counts[i]; j++)
                {
                    AddProduct(products);
                }
            }
        }
    }
    public void ResetWareHouse()
    {
        SaveSystemWareHouse.ResetData();
    }
}

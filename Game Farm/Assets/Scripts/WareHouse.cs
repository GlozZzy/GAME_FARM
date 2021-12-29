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

    public bool AddProduct(Product product)
    {
        if (curspace < maxspace)
        {
            products.Add(product);

            string a = "Warehouse" + product.pname;
            GameObject obj = GameObject.FindGameObjectWithTag(a);
            var textcount = obj.transform.Find("count");
            int count = int.Parse(textcount.gameObject.GetComponent<Text>().text.Split(' ')[1]);
            textcount.gameObject.GetComponent<Text>().text = "count: " + (count + 1);

            curspace++;
            spaceText.text = "Space: " + curspace + "/" + maxspace;
            return true;
        }
        else return false;
    }

    public void SellProduct(Text product)
    {
        int ind = -1;
        for (int i = 0; i < products.Count; i++)
            if (products[i].pname == product.text)
                ind = i;
        if (ind >= 0)
        {
            player.Transaction(products[ind].sell_price);

            string a = "Warehouse" + products[ind].pname;
            GameObject obj = GameObject.FindGameObjectWithTag(a);
            var textcount = obj.transform.Find("count");
            int count = int.Parse(textcount.gameObject.GetComponent<Text>().text.Split(' ')[1]);
            textcount.gameObject.GetComponent<Text>().text = "count: " + (count - 1);

            curspace--;
            spaceText.text = "Space: " + curspace + "/" + maxspace;
            products.RemoveAt(ind);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (MenuManager.GameIsPaused) return;
        OpenMenu();
    }
}

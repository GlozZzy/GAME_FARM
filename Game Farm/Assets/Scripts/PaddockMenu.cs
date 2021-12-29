using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddockMenu : MonoBehaviour
{
    // Start is called before the first frame update
    Paddock paddock;
    Canvas canvas;
    Player player;
    FieldMenu cellMenu;
    public Text count;
    public Text price;
    public Text capacity;
    WareHouse warehouse;

    //Template product;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        player = FindObjectOfType<Player>();
        warehouse = FindObjectOfType<WareHouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paddock)
        {
            capacity.text = "Space: " + paddock.products.Count + "/10";
            price.text = 10 + "";
            count.text = "count" + paddock.numOfAnimals + "/10";
        }
    }
    public void Collect()
    {
        for (int i = 0; i < paddock.products.Count; i++)
        {
            var product = new Product();
            product.pname = paddock.animal.productName;
            warehouse.AddProduct(product);
        }
    }

    public void CloseMenu()
    {
        canvas.enabled = false;
        paddock = null;
    }
    public void Release()
    {
        Destroy(paddock.gameObject);
        CloseMenu();

        Debug.Log(paddock);
    }
    public void AddAnimal()
    {
        if (paddock.numOfAnimals < 10)
        {
            if (player.Transaction(-paddock.animal.addAnimalPrice))
            {
                paddock.numOfAnimals++;
            }
            else
            {
                cellMenu = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
            }
        }
    }
    public void FeedAnimal()
    {
        //warehouse.GetProduct(paddock.numOfAniamls);
        paddock.hungry_timer = paddock.animal.hungryTime;

    }
    public void KillAnimal()
    {
        if (paddock.numOfAnimals > 0)
        {
            paddock.numOfAnimals--;
        }
        paddock.hungry_timer = paddock.animal.hungryTime;
        var product = new Product();
        product.pname = paddock.animal.meatName;
        warehouse.AddProduct(product);


    }
    public void Open(Paddock p)
    {
        if (paddock) paddock.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        canvas.enabled = true;
        paddock = p;
    }
}

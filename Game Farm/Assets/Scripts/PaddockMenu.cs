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
    public Text foodCount;

    public Text animName;
    public Text productName;

    WareHouse warehouse;
    public Text timeToDie;
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
            price.text = "Add Animal(-" + paddock.animal.addAnimalPrice +"m)";
            count.text = "count " + paddock.numOfAnimals + "/10";
            foodCount.text = "Feed (" + paddock.numOfAnimals + ")";
            timeToDie.text = "Time to die next animal: " + System.Convert.ToInt32(paddock.hungry_timer);
            if (paddock.hungry_timer <= 5)
                timeToDie.color = Color.red;
            else timeToDie.color = Color.white;
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
                paddock.hungry_timer = paddock.animal.hungryTime;
            }
            else
            {
                cellMenu = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
                cellMenu.Open();
            }
        }
    }
    public void FeedAnimal()
    {
        if (warehouse.TakeProduct(paddock.animal.feed, paddock.numOfAnimals))
        paddock.hungry_timer = paddock.animal.hungryTime;

    }
    public void KillAnimal()
    {
        if (paddock.numOfAnimals > 0)
        {
            paddock.numOfAnimals--;
            paddock.hungry_timer = paddock.animal.hungryTime;
            var product = new Product();
            product.pname = paddock.animal.meatName;
            warehouse.AddProduct(product);
        }

    }
    public void Open(Paddock p)
    {
        if (paddock) paddock.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        canvas.enabled = true;
        paddock = p;

        animName.text = paddock.animal.nameAnim;
        productName.text = paddock.animal.productName;
    }
}

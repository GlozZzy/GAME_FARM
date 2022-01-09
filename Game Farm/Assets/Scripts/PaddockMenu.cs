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
    public Text count;
    public Text price;
    public Text capacity;
    public Text foodCount;
    public GameObject prodSprite;
    public GameObject animSprite;
    public GameObject fprodSprite;
    public Text feedName;
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
            capacity.text = "Space: " + paddock.numOfProducts + "/10";
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
        int temp=0;
        for (int i = 0; i < paddock.numOfProducts; i++)
        {
            
            var product = new Product();
            product.pname = paddock.animal.productName;
            if (warehouse.AddProduct(product) == false)
            {
                break;
            }
                temp = i + 1;

        }
        paddock.numOfProducts -= temp;
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
                paddock.timer = paddock.animal.productCreationTime;
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
            var product = new Product();
            product.pname = "Meat";
            if (warehouse.AddProduct(product))
            {
                paddock.numOfAnimals--;
                paddock.hungry_timer = paddock.animal.hungryTime;
            }
        }

    }
    public void Open(Paddock p)
    {
        if (paddock) paddock.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        canvas.enabled = true;
        paddock = p;

        animName.text = paddock.animal.nameAnim;
        productName.text = paddock.animal.productName;
        prodSprite.GetComponent<RawImage>().texture = paddock.animal.textures[0];
        fprodSprite.GetComponent<RawImage>().texture = paddock.animal.textures[2];
        feedName.text = paddock.animal.feed;
    }
}

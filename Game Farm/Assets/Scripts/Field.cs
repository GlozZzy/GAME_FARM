using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    Player player;
    FieldMenu cellMenu;
    // Product fields
    public GameObject[] Fields;
    GameObject fieildObject;
    Product product;
    WareHouse warehouse;

    //public Sprite[] plantStages;
    public bool isBlocked;
    string plantnName;
    int plantStage = 0;
    float timeBtwStages = 5f;
    float timer;
    public bool choosen;


    // Start is called before the first frame update
    void Start()
    {
        choosen = false;
        if (!isBlocked)
        {
            plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        player = GameObject.Find("PlayerInfo").GetComponent<Player>();
        warehouse = GameObject.Find("WareHouse").GetComponent<WareHouse>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!choosen) GetComponent<SpriteRenderer>().color = Color.white;
        if (isPlanted && !isBlocked) 
        { 
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage < product.plantStages.Length - 1)
            {
                plantStage++;
                timer = product.phases[plantStage];
                UpdatePlant();
            }
        }
    }


    private void OnMouseDown()
    {
        if (MenuManager.GameIsPaused) return;

        if (isPlanted)
        {
            if (plantStage >= product.plantStages.Length - 2) Harvest();
            return;
        }

        if (!isBlocked) OpenMenu();
        else OpenBlockedMenu();
    }

    private void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        if (plantStage == product.plantStages.Length - 2)
        {
            player.Transaction(product.price);
            player.GetExp(product.exp);
            warehouse.AddProduct(product);            
        }
        Destroy(fieildObject);
    }

    public void Plant(string str)
    {
        plantnName = str;
        FieldInstructor(str);
        
        if (player.Transaction(-10))
        {
            isPlanted = true;
            plantStage = 0;
            UpdatePlant();
            timer = product.phases[plantStage];
            plant.gameObject.SetActive(true);
        }
    }

    private void UpdatePlant()
    {
        plant.sprite = product.plantStages[plantStage];
    }

    public void OpenMenu()
    {
        cellMenu = GameObject.Find("FieldMenu").GetComponent<FieldMenu>();
        choosen = true;
        cellMenu.Open(this);
    }

    public void OpenBlockedMenu()
    {
        cellMenu = GameObject.Find("BlockedCellMenu").GetComponent<FieldMenu>();
        cellMenu.Open(this);
    }

    public void Enlarge()
    {
        print("EnlargeTheTerritory");
    }
    private void FieldInstructor(string t)
    {
        if (t == "Carrot")
        {
            fieildObject = Instantiate(Fields[0], new Vector3(0f, 0f, 0f), Quaternion.identity);
            product = fieildObject.GetComponent<Carrot>();
        }
        else if (t == "Wheat")
        {
            fieildObject = Instantiate(Fields[1], new Vector3(0f, 0f, 0f), Quaternion.identity);
            product = fieildObject.GetComponent<Wheat>();
        }
        fieildObject.transform.parent = this.transform;
    }
    private void ReleaseField()
    {
        product = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerClickHandler
{
    [System.NonSerialized]
    public bool isPlanted = false;
    SpriteRenderer plant;
    Player player;
    FieldMenu cellMenu;

    // Product fields
    public GameObject[] Fields;
    GameObject fieildObject;
    [System.NonSerialized]
    public Product product;
    WareHouse warehouse;

    public Sprite[] plantStages;
    public bool isBlocked;

    [System.NonSerialized]
    public int plantStage = 0;
    [System.NonSerialized]
    public float timer;
    [System.NonSerialized]
    public bool choosen;
    bool started = false;
    // Start is called before the first frame update
    //������ ����������!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //� ����� ������ ��� ������� ���� � ����������� �����. ������ � ������� ���� ���� 5 ��������� ��������. 0 ������ - ������ ������ ���� (������� ����). 1 ������ - ������ ������� ���� (������� ����)
    //��������� ������� ��� � ������ ������ ���������� ������ ������ ����� �������� 

    void Start()
    {
        if (started) return;
        choosen = false;
        plant = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        warehouse = FindObjectOfType<WareHouse>();
        if (!isBlocked)
        {
            plant.sprite = plantStages[1];
        } 
        else
        {
            plant.sprite = plantStages[0];
        }
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Hello");
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
        plant.sprite = plantStages[1];
        if (plantStage == product.plantStages.Length - 2)
        {
            player.Transaction(product.sell_price);
            player.GetExp(product.exp);
            warehouse.AddProduct(product);
        }
        Destroy(fieildObject);
    }

    public void Plant(string str)
    {
        FieldInstructor(str);

        if (player.Transaction(-product.buy_price))
        {
            isPlanted = true;
            plantStage = 0;
            UpdatePlant();
            timer = product.phases[plantStage];
        }
        else
        {
            cellMenu = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
            choosen = true;
            cellMenu.Open(this);
        }
    }

    private void FieldInstructor(string t)
    {
        if (t == "Carrot")
        {
            fieildObject = Instantiate(Fields[0], new Vector3(0f, 0f, 0f), Quaternion.identity);
            product = fieildObject.GetComponent<Product>();
        }
        else if (t == "Wheat")
        {
            fieildObject = Instantiate(Fields[1], new Vector3(0f, 0f, 0f), Quaternion.identity);
            product = fieildObject.GetComponent<Product>();
        }
        fieildObject.transform.parent = this.transform;
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
        if (player.Transaction(-10)) //�������� ������� �� �����
        {
            choosen = false; //������� ���������
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.375f, 0.65f, 0.0375f), 0.01f); //�������� ���� �� ������ �� �����-�� �����������
            if (colliders.Length == 0)
            {
                GameObject newFieldRU = Instantiate(gameObject, transform.position + new Vector3(0.65f, 0.375f, 0.0375f), transform.rotation);//������� ����� �������
                newFieldRU.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); //������ ���� �� �������, ��� ���� ������� � ����� ����� ���� ��� � ����������
                newFieldRU.transform.parent = this.transform.parent;
            }

            colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0.65f, 0.0375f), 0.01f); //������ ��� ���� ������ �����
            if (colliders.Length == 0)
            {
                GameObject newFieldLU = Instantiate(gameObject, transform.position + new Vector3(-0.65f, 0.375f, 0.0375f), transform.rotation);
                newFieldLU.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                newFieldLU.transform.parent = this.transform.parent;
            }

            colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.375f, 0f), 0.01f);
            if (colliders.Length == 0)
            {
                GameObject newFieldRD = Instantiate(gameObject, transform.position + new Vector3(0.65f, -0.375f, -0.0375f), transform.rotation);
                newFieldRD.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                newFieldRD.transform.parent = this.transform.parent;
            }

            colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0f, 0.0375f), 0.01f);
            if (colliders.Length == 0)
            {
                GameObject newFieldLD = Instantiate(gameObject, transform.position + new Vector3(-0.65f, -0.375f, -0.0375f), transform.rotation);
                newFieldLD.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                newFieldLD.transform.parent = this.transform.parent;
            }

            isBlocked = false; //������ ���� �� ������� ������
            plantStage = 1;
            plant.sprite = plantStages[plantStage];
        } 
        else
        {
            cellMenu = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
            choosen = true;
            cellMenu.Open(this);
        }
    }
    private void ReleaseField()
    {
        product = null;
    }

    public void SaveField()
    {
        SaveSystemFields.SaveField(this);
    }

    public void LoadFields()
    {
        SaveSystemFields.LoadField();
    }

    public void LoadField(FieldData data, bool start = false)
    {
        isBlocked = data.isBlocked;
        Start();
        started = true;


        plantStage = data.stage;
        timer = data.timer;
        isPlanted = data.isPlanted;
        if (data.nameProduct != null)
        {
            FieldInstructor(data.nameProduct);
            plant.sprite = product.plantStages[plantStage];
            
        }
    }

    public void ResetField()
    {
        SaveSystemFields.ResetData();
    }

}

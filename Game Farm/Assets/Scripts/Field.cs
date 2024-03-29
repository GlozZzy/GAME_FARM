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
    public bool isAlien;

    [System.NonSerialized]
    public int plantStage = 0;

    [System.NonSerialized]
    public float growTimer;
    [System.NonSerialized]
    public float waterTimer;
    [System.NonSerialized]
    public float deathTimer;

    [System.NonSerialized]
    public bool choosen;
    bool started = false;

    public float timeTillWater = 10f;
    public float timeTillDeath = 10f;
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
        deathTimer = timeTillDeath;
        waterTimer = timeTillWater;
        if (!isBlocked)
        {
            plant.sprite = plantStages[1];
        } 
        else
        {
            plant.sprite = plantStages[0];
        }
    }
    public void Check()
    {
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
            if (plantStage == product.plantStages.Length - 2)
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            else
                this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            if (waterTimer < 0)
            {
                deathTimer -= Time.deltaTime;

                if (plantStage < product.plantStages.Length-2) 
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                else
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                if (deathTimer < 0)
                {
                    plantStage = product.plantStages.Length - 1;
                    plant.sprite = product.plantStages[plantStage];
                }
            }
            else
            {
                growTimer -= Time.deltaTime;
                waterTimer -= Time.deltaTime;

                if (growTimer < 0 && plantStage < product.plantStages.Length - 1)
                {
                    plantStage++;
                    growTimer = product.phases[plantStage];
                    UpdatePlant();
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (MenuManager.GameIsPaused) return;

        if (isPlanted)
        {
            if (waterTimer > 0)
            {
                if (plantStage >= product.plantStages.Length - 2) Harvest();
            }
            else
            {
                if (plantStage < product.plantStages.Length - 1)
                    Water();
                else
                    Harvest();
            }
            return;
        }

        if (!isBlocked && !isAlien)
        {
            GameObject.Find("BlockedCellMenu").GetComponent<FieldMenu>().Close();
            OpenMenu();
        }
        if (isAlien) 
        {
            GameObject.Find("FieldMenu").GetComponent<FieldMenu>().Close(); 
            OpenBlockedMenu(); 
        }
    }

    private void Water()
    {
        if (player.Transaction(-2))
        {
            waterTimer = timeTillWater;
            deathTimer = timeTillDeath;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void Harvest()
    {
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        isPlanted = false;
        plant.sprite = plantStages[1];
        if (plantStage == product.plantStages.Length - 2)
        {
            //player.Transaction(product.sell_price);
            player.GetExp(product.exp);
            warehouse.AddProduct(product);
            Destroy(fieildObject);
        }
        else if (plantStage == product.plantStages.Length - 1)
            if (player.Transaction(-1)) Destroy(fieildObject);
        
    }

    public void Plant(string str)
    {
        deathTimer = timeTillDeath;
        waterTimer = timeTillWater;
        FieldInstructor(str);
        if (player.Transaction(-product.buy_price))
        {
            isPlanted = true;
            plantStage = 0;
            UpdatePlant();
            growTimer = product.phases[plantStage];
        }
        else
        {
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
            isAlien = false;
            plantStage = 1;
            plant.sprite = plantStages[plantStage];
        } 
        else
        {
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
        isAlien = data.isAlien;
        Start();
        started = true;

        plantStage = data.stage;
        growTimer = data.timer;
        waterTimer = data.waterTimer;
        deathTimer = data.deathTimer;
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
    public void SellTheField()
    {
        player.money += 8;
        isBlocked = true;
        isAlien = true;
        plant.sprite = plantStages[0];
        choosen = false; //������� ���������
        //RaycastHit2D rayCastHit = Physics2D.Raycast(Vector3.left, transform.position);
        //Debug.Log(rayCastHit.collider.name);
        //if (rayCastHit.collider != null)
        //{
        //    Debug.Log(rayCastHit.collider.gameObject.transform.position);
        //    if (rayCastHit.collider.gameObject.tag == "Plot")
        //    {
        //        rayCastHit.collider.gameObject.GetComponent<Field>().isBlocked = true;
        //        rayCastHit.collider.gameObject.GetComponent<Field>().plant.sprite = plantStages[0];
        //    }
        //}
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.375f, 0.65f, 0.0375f), 0.01f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().NeedToDelete())
                Destroy(collider.gameObject);
        }
        colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0.65f, 0.0375f), 0.01f);//�������� ���� �� ������ �� �����-�� �����������
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().NeedToDelete())
                Destroy(collider.gameObject);
        }
        colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.375f, 0f), 0.01f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().NeedToDelete())
                Destroy(collider.gameObject);
        }
        colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0f, 0.0375f), 0.01f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().NeedToDelete())
                Destroy(collider.gameObject);
        }
        if (NeedToDelete())
        {
            Destroy(this.gameObject);
        }
    }
    public bool NeedToDelete()
    {
        if (!isAlien)
            return false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.375f, 0.65f, 0.0375f), 0.01f); //�������� ���� �� ������ �� �����-�� �����������
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().isAlien == false)
                return false;
        }

        colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0.65f, 0.0375f), 0.01f); //������ ��� ���� ������ �����
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().isAlien == false)
                return false;
        }

        colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.375f, 0f), 0.01f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().isAlien == false)
                return false;
        }

        colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0f, 0.0375f), 0.01f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;
            if (collider.gameObject.GetComponent<Field>().isAlien == false)
                return false;
        }
        return true;
    }




}

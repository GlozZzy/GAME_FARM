using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    Player player;
    FieldMenu cellMenu;

    public Sprite[] plantStages;
    public bool isBlocked;

    int plantStage = 0;
    float timeBtwStages = 5f;
    float timer;
    public bool choosen;

    // Start is called before the first frame update
    //������ ����������!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //� ����� ������ ��� ������� ���� � ����������� �����. ������ � ������� ���� ���� 5 ��������� ��������. 0 ������ - ������ ������ ���� (������� ����). 1 ������ - ������ ������� ���� (������� ����)
    //��������� ������� ��� � ������ ������ ���������� ������ ������ ����� �������� 

    void Start()
    {
        choosen = false;
        plant = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
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
            if (timer < 0 && plantStage < plantStages.Length - 1)
            {
                timer = timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }


    private void OnMouseDown()
    {
        if (MenuManager.GameIsPaused) return;

        if (isPlanted)
        {
            if (plantStage == plantStages.Length - 1) Harvest();
            return;
        }

        if (!isBlocked) OpenMenu();
        else OpenBlockedMenu();
    }

    private void Harvest()
    {
        isPlanted = false;
        player.Transaction(20);
        player.GetExp(10);
        plantStage = 1;
        UpdatePlant();
    }

    public void Plant()
    {
        if (player.Transaction(-10))
        {
            isPlanted = true;
            plantStage = 2;
            UpdatePlant();
            timer = timeBtwStages;
        }
        else
        {
            cellMenu = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
            choosen = true;
            cellMenu.Open(this);
        }
    }

    private void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
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
            }

            colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0.65f, 0.0375f), 0.01f); //������ ��� ���� ������ �����
            if (colliders.Length == 0)
            {
                GameObject newFieldLU = Instantiate(gameObject, transform.position + new Vector3(-0.65f, 0.375f, 0.0375f), transform.rotation);
                newFieldLU.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }

            colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.375f, 0f), 0.01f);
            if (colliders.Length == 0)
            {
                GameObject newFieldRD = Instantiate(gameObject, transform.position + new Vector3(0.65f, -0.375f, -0.0375f), transform.rotation);
                newFieldRD.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }

            colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.375f, 0f, 0.0375f), 0.01f);
            if (colliders.Length == 0)
            {
                GameObject newFieldLD = Instantiate(gameObject, transform.position + new Vector3(-0.65f, -0.375f, -0.0375f), transform.rotation);
                newFieldLD.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
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
}

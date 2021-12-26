using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;
    Player player;
    FieldMenu cellMenu;

    //--------------------------------
    /*public GameObject farm;
    public List<SpriteRenderer> listOfFields = new List<SpriteRenderer>();*/

    public Sprite[] plantStages;
    public bool isBlocked;

    public int plantStage = 0;
    float timeBtwStages = 1f;       //--------------------------------------------
    float timer;
    public bool choosen;

    public void SaveField()
    {
        SaveSystemFields.SaveField(this);
    }
    public void LoadField()
    {
        FieldData data = SaveSystemFields.LoadField();

        plantStage = data.stage;
        plant = data.plant;
        plant.sprite = plantStages[plantStage];
    }
    public void ResetPlayer()
    {
        SaveSystem.ResetData();
    }

    // Start is called before the first frame update
    void Start()
    {
        //сохранение всех клеток             //--------------------------------
        /*for (int i = 0; i < farm.transform.childCount; i++)
        {
            listOfFields.Add(farm.transform.GetChild(i).GetComponent<SpriteRenderer>());
        }*/

        choosen = false;
        if (!isBlocked)
        {
            plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        player = GameObject.Find("PlayerInfo").GetComponent<Player>();
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
        plant.gameObject.SetActive(false);
        player.Transaction(20);
        player.GetExp(10);
    }

    public void Plant()
    {
        if (player.Transaction(-10))
        {
            isPlanted = true;
            plantStage = 0;
            UpdatePlant();
            timer = timeBtwStages;
            plant.gameObject.SetActive(true);
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
        print("EnlargeTheTerritory");
    }

    public void ChangeState(int state, SpriteRenderer p)
    {
        print("pp: " + plantStages.Length);
       // p.sprite = plantStages[state];
    }
}

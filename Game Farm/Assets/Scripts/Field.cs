using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    Player player;
    CellMenu cellMenu;

    public Sprite[] plantStages;
    public bool isBlocked;

    int plantStage = 0;
    float timeBtwStages = 5f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (!isBlocked)
        {
            plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        player = GameObject.Find("PlayerInfo").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
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
        cellMenu = GameObject.Find("CellMenu").GetComponent<CellMenu>();
        cellMenu.Open(this);
    }

    public void OpenBlockedMenu()
    {
        cellMenu = GameObject.Find("BlockedCellMenu").GetComponent<CellMenu>();
        cellMenu.Open(this);
    }

    public void Enlarge()
    {
        print("EnlargeTheTerritory");
    }
}

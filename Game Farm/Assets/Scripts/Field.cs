using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    BoxCollider2D plantCollider;
    Player player;

    public Sprite[] plantStages;
    int plantStage = 0;
    float timeBtwStages = 5f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        player = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted) 
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
        if (isPlanted)
        {
            if (plantStage == plantStages.Length - 1) Harvest();

        }
        else if (10 <= player.money)
        {
            Plant();
        }
    }
    private void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        player.Transaction(20);
    }

    private void Plant()
    {
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = timeBtwStages;
        plant.gameObject.SetActive(true);
        player.Transaction(-10);


    }

    private void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y/2);
    }

}

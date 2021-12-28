using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddockMenu : MonoBehaviour
{
    // Start is called before the first frame update
    Paddock paddock;
    Canvas canvas;
    Player player;
    FieldMenu cellMenu;
    //Template product;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paddock)
        {

        }
    }
    public void Collect()
    {

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
        if (paddock.animal.numOfAnimals < 10)
        {
            if (player.Transaction(-paddock.animal.addAnimalPrice))
            {
                paddock.animal.numOfAnimals++;
            }
            else
            {
                cellMenu = GameObject.Find("NotEnoughMoney").GetComponent<FieldMenu>();
            }
        }
    }
    public void FeedAnimal()
    {
        if (paddock.animal.numOfAnimals >0)
        {
            paddock.animal.numOfAnimals--;
        }
    }
    public void KillAnimal()
    {
        paddock.timer = paddock.animal.hungryTime;
    }
    public void Open(Paddock p)
    {
        if (paddock) paddock.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        canvas.enabled = true;
        paddock = p;
    }
}

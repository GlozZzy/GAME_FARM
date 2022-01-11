using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Paddock : MonoBehaviour, IPointerClickHandler
{
    PaddockMenu paddockMenu;
    [System.NonSerialized]
    public Animals animal;
    [System.NonSerialized]
    public float timer;
    [System.NonSerialized]
    public float hungry_timer;
    public int numOfProducts;
    public int numOfAnimals;
    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponent<Animals>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(numOfAnimals == 0)
        {
            timer = 0;
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0 && numOfProducts < 10)
        {
            for(int i=0; i<numOfAnimals && numOfProducts < 10; i++)
            {
                numOfProducts++;
            }
            timer = animal.productCreationTime;
        }
        hungry_timer -= Time.deltaTime;
        if (hungry_timer < 0 && numOfAnimals > 0)
        {
            numOfAnimals--;
            hungry_timer = animal.hungryTime;
        }
    }
    public void OpenMenu()
    {
        paddockMenu = FindObjectOfType<PaddockMenu>();
        
        paddockMenu.Open(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OpenMenu();
    }

    public void SavePaddock()
    {
        SaveSystemPaddock.SavePaddock(this);
    }
    public void LoadPaddocks()
    {
        SaveSystemPaddock.LoadPaddock();
    }
    public void LoadPaddock(PaddockData data)   
    {
        timer = data.timer;
        numOfProducts = data.numOfProducts;
        numOfAnimals = data.numOfAnimals;
        hungry_timer = data.hungry_timer;
    }
    public void ResetPaddock()
    {
        SaveSystemPaddock.ResetData();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Paddock : MonoBehaviour, IPointerClickHandler
{
    PaddockMenu paddockMenu;
    [System.NonSerialized]
    public Animals animal;
    public float timer;
    public float hungry_timer;
    public List<Product> products;
    public int numOfAnimals;
    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponent<Animals>();   
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && products.Count < 10)
        {
            for(int i=0; i<numOfAnimals && products.Count < 10; i++)
            {
                products.Add(new Product());
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
}

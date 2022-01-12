using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ProductionBuilding : MonoBehaviour, IPointerClickHandler
{
    public Recipies[] recipies;
    public Canvas canvasMenu;
    private Recipies currentRecipie = null;
    private float timer;
    private bool addProduct = false;
    public WareHouse wareHouse;
    // Start is called before the first frame update

   
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }
        else if (addProduct == true)
        {
            addProduct = false;
            wareHouse.AddProduct(currentRecipie.productToCreate);
            currentRecipie = null;
            Debug.Log("Продукт создан!");
        }
    }
    public void CreateProduct(Recipies recipie)
    {
        Debug.Log("заказ пришел");
        if (currentRecipie == null)
        {
            Debug.Log("заказ выполняется");
            currentRecipie = recipie;
            timer = currentRecipie.timeToCreate;
            for (int i = 0; i < currentRecipie.ingridients.Length; i++)
            {
                if (!wareHouse.TakeProduct(currentRecipie.ingridients[i].product.Pname, currentRecipie.ingridients[i].numberOfProducts))
                {
                    currentRecipie = null;
                    break;
                }
                Debug.Log(currentRecipie.ingridients[i].product.Pname + " Удален");
            }
            addProduct = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<BuildingsMenu>().Open(this, recipies, canvasMenu);
    }
}

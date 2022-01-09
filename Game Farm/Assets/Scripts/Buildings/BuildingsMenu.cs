using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsMenu : MonoBehaviour
{
    ProductionBuilding building;
    Player player;
    FieldMenu cellMenu;
    Canvas canvas;
    GameObject ingridients;
    public GameObject originalMenu;
    private int shift = 1;

    public void Open(ProductionBuilding p, Recipies[] recipies, Canvas can)
    {
        if (building) building.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        canvas = can;
        canvas.enabled = true;
        building = p;
        ingridients = Instantiate(originalMenu, originalMenu.GetComponent<RectTransform>().localPosition, originalMenu.transform.rotation);
        ingridients.transform.parent = this.transform;

        ingridients.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        ingridients.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);

        GameObject initialRecipieField = ingridients.transform.GetChild(2).gameObject;
        Debug.Log(recipies.Length);

        for (int i = 0; i < recipies.Length; i++)
        {
            if (i == 0)
            {
                ingridients.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = recipies[i].productToCreate.pname;
                ingridients.transform.GetChild(2).GetChild(3).GetComponent<CreateButton>().recipie = recipies[i];
                ingridients.transform.GetChild(2).GetChild(3).GetComponent<CreateButton>().building = building;
                ingridients.transform.GetChild(2).GetChild(3).GetComponent<CreateButton>().buildingsMenu = this;
                ingridients.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>().text = recipies[0].ingridients[0].product.Pname;
                ingridients.transform.GetChild(2).GetChild(2).GetChild(2).GetComponent<Text>().text = Convert.ToString(recipies[0].ingridients[0].numberOfProducts);
                shift++;
                for (int j = 1; j < recipies[0].ingridients.Length; j++) 
                {
                    GameObject tempObj = Instantiate(initialRecipieField.transform.GetChild(2).gameObject, new Vector3(0, 0, 0), ingridients.transform.rotation);
                    Debug.Log(tempObj.transform.position);
                    
                    tempObj.transform.GetChild(1).GetComponent<Text>().text = recipies[i].ingridients[j].product.Pname;
                    tempObj.transform.GetChild(2).GetComponent<Text>().text = Convert.ToString(recipies[i].ingridients[j].numberOfProducts);
                    tempObj.transform.parent = ingridients.transform.GetChild(2);
                    Debug.Log(tempObj.transform.position);
                    tempObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(10, -60 * (j+1));
                    Debug.Log(tempObj.transform.position);
                    shift++;
                }

            }
            else
            {
                GameObject tempRecipie = Instantiate(initialRecipieField, new Vector2(0,0), ingridients.transform.rotation);
                shift++;
                tempRecipie.GetComponent<RectTransform>().anchoredPosition = new Vector3(145,595 - (20 + (60 * shift)), 0);
                tempRecipie.transform.GetChild(1).GetComponent<Text>().text = recipies[i].productToCreate.pname;
                tempRecipie.transform.GetChild(3).GetComponent<CreateButton>().recipie = recipies[i];
                tempRecipie.transform.GetChild(3).GetComponent<CreateButton>().building = building;
                tempRecipie.transform.GetChild(3).GetComponent<CreateButton>().buildingsMenu = this;

                tempRecipie.transform.parent = ingridients.transform;

                tempRecipie.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = recipies[i].ingridients[0].product.Pname;
                tempRecipie.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = Convert.ToString(recipies[i].ingridients[0].numberOfProducts);
                shift++;
                Debug.Log(recipies[i].ingridients.Length);
                for (int j = 1; j < recipies[i].ingridients.Length; j++)
                {
                    GameObject tempObj = Instantiate(tempRecipie.transform.GetChild(2).gameObject, new Vector3(0, 0, 0), ingridients.transform.rotation);
                    tempObj.transform.GetChild(1).GetComponent<Text>().text = recipies[i].ingridients[j].product.Pname;
                    tempObj.transform.GetChild(2).GetComponent<Text>().text = Convert.ToString(recipies[i].ingridients[j].numberOfProducts);
                    tempObj.transform.parent = tempRecipie.transform;
                    tempObj.GetComponent<RectTransform>().localPosition = new Vector2(10, -60 * (j + 1));
                    shift++;
                }

            }
        }

    }

    public void CloseMenu()
    {
        Destroy(ingridients);
        shift = 1;
        canvas.enabled = false;
        building = null;
    }
}

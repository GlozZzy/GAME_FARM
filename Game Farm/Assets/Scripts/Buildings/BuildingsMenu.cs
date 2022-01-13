using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsMenu : MonoBehaviour
{
    ProductionBuilding building;
    Player player;
    Canvas canvas;
    GameObject ingridients;
    public GameObject originalMenu;
    private int shift = 1;

    private void Update()
    {
        if (building)
        {
            if (building.timer > 0.1f)
            {
                ingridients.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = Convert.ToString(Math.Round(building.timer, 2));
            }
            else
            {
                ingridients.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "Свободно!";
            }
        }
    }
    public void Open(ProductionBuilding p, Recipies[] recipies, Canvas can)
    {
        if (building) 
        {
            building.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            CloseMenu();
        }
        canvas = can;
        canvas.enabled = true;
        building = p;
        player = FindObjectOfType<Player>();
        ingridients = Instantiate(originalMenu, originalMenu.GetComponent<RectTransform>().localPosition, originalMenu.transform.rotation);
        ingridients.transform.parent = this.transform;

        ingridients.GetComponent<RectTransform>().offsetMin = new Vector2(originalMenu.GetComponent<RectTransform>().offsetMin.x, 0f);
        ingridients.GetComponent<RectTransform>().offsetMax = new Vector2(originalMenu.GetComponent<RectTransform>().offsetMax.x, 0f);

        GameObject initialRecipieField = ingridients.transform.GetChild(2).gameObject;

        for (int i = 0; i < recipies.Length; i++)
        {
            if (i == 0)
            {
                ingridients.transform.GetChild(2).GetChild(0).GetComponent<RawImage>().texture = recipies[i].productToCreate.texture;
                ingridients.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = recipies[i].productToCreate.pname;
                ingridients.transform.GetChild(2).GetChild(3).GetComponent<CreateButton>().recipie = recipies[i];
                ingridients.transform.GetChild(2).GetChild(3).GetComponent<CreateButton>().building = building;
                ingridients.transform.GetChild(2).GetChild(3).GetComponent<CreateButton>().buildingsMenu = this;
                ingridients.transform.GetChild(1).GetComponent<CreateButton>().buildingsMenu = this;
                ingridients.transform.GetChild(3).GetComponent<CreateButton>().buildingsMenu = this;
                ingridients.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<RawImage>().texture = recipies[0].ingridients[0].product.texture;
                ingridients.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>().text = recipies[0].ingridients[0].product.Pname;
                ingridients.transform.GetChild(2).GetChild(2).GetChild(2).GetComponent<Text>().text = Convert.ToString(recipies[0].ingridients[0].numberOfProducts);
                shift++;
                for (int j = 1; j < recipies[0].ingridients.Length; j++) 
                {
                    GameObject tempObj = Instantiate(initialRecipieField.transform.GetChild(2).gameObject, new Vector3(0, 0, 0), ingridients.transform.rotation);
                    Debug.Log(tempObj.transform.position);
                    tempObj.transform.GetChild(0).GetComponent<RawImage>().texture = recipies[i].ingridients[j].product.texture;
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
                tempRecipie.GetComponent<RectTransform>().anchoredPosition = new Vector3(145,Screen.height - (20 + 60 * shift), 0);
                tempRecipie.transform.GetChild(0).GetComponent<RawImage>().texture = recipies[i].productToCreate.texture;
                tempRecipie.transform.GetChild(1).GetComponent<Text>().text = recipies[i].productToCreate.pname;
                tempRecipie.transform.GetChild(3).GetComponent<CreateButton>().recipie = recipies[i];
                tempRecipie.transform.GetChild(3).GetComponent<CreateButton>().building = building;
                tempRecipie.transform.GetChild(3).GetComponent<CreateButton>().buildingsMenu = this;

                tempRecipie.transform.parent = ingridients.transform;
                tempRecipie.transform.GetChild(2).GetChild(0).GetComponent<RawImage>().texture = recipies[i].ingridients[0].product.texture;
                tempRecipie.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = recipies[i].ingridients[0].product.Pname;
                tempRecipie.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = Convert.ToString(recipies[i].ingridients[0].numberOfProducts);
                shift++;
                for (int j = 1; j < recipies[i].ingridients.Length; j++)
                {
                    GameObject tempObj = Instantiate(tempRecipie.transform.GetChild(2).gameObject, new Vector3(0, 0, 0), ingridients.transform.rotation);
                    tempObj.transform.GetChild(0).GetComponent<RawImage>().texture = recipies[i].ingridients[j].product.texture;
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

    public void DestroyBuilding()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(building.transform.position, 0.2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Plot")
                continue;

            collider.gameObject.GetComponent<Field>().isBlocked = false;
            collider.gameObject.GetComponent<Field>().Check();
        }
        
        player.Transaction(building.GetComponentInParent<Building>().sell_price);
        Destroy(building.gameObject);
        CloseMenu();
    }
}

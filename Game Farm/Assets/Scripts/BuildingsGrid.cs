using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid: MonoBehaviour
{
    public Canvas Shop;
    public Vector2Int GridSize = new Vector2Int(10, 10);
    public Player player;
    private Building flyingBuilding;
    public Canvas NEM;

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (buildingPrefab.buy_price <= player.money)
        {
            Shop.gameObject.SetActive(false);
            if (flyingBuilding != null)
            {
                Destroy(flyingBuilding.gameObject);
            }

            flyingBuilding = Instantiate(buildingPrefab);
            if (flyingBuilding)
            {
                flyingBuilding.MainRenderer.enabled = true;
                flyingBuilding.transform.position = new Vector2(0.65f, -0.45f);
            }
        }
        else
            NEM.enabled = true;
    }

    private void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        if (flyingBuilding != null)
        {
            
            flyingBuilding.transform.position = new Vector2(worldPosition.x - worldPosition.x % 0.65f-0.4f, worldPosition.y - worldPosition.y % 0.375f + 0.125f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            int s = 0;
            if (!flyingBuilding) return;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(flyingBuilding.transform.position, 0.2f);
            if (colliders.Length==4)
            {
                Debug.Log("4");
                foreach (Collider2D col in colliders)
                {
                    if (col.GetComponentInParent<Field>().isBlocked == false && col.GetComponentInParent<Field>().isPlanted == false)
                        s++;
                    Debug.Log(col.GetComponentInParent<Field>().transform.position);
                }
                if (s == 4)
                {

                    foreach (Collider2D col in colliders)
                    {
                        col.GetComponentInParent<Field>().isBlocked = true;
                        col.GetComponentInParent<Field>().Check();

                    }
                    if (flyingBuilding)
                    {
                        flyingBuilding.colaider.enabled = true;
                        PlaceFlyingBuilding(flyingBuilding.transform.position.x, flyingBuilding.transform.position.y);
                    }
                }

               
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(flyingBuilding.gameObject);
            flyingBuilding = null;
            Shop.gameObject.SetActive(true);
        }

    }


    private void PlaceFlyingBuilding(float placeX, float placeY)
    {
        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }
}

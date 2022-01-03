using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid: MonoBehaviour
{
    public Canvas Shop;
    public Vector2Int GridSize = new Vector2Int(10, 10);
    public Player player;
    private Building flyingBuilding;
    private Camera mainCamera;
    public Canvas NEM;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (buildingPrefab.buy_price <= player.money)
        {
            Shop.enabled = false;
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
        if (flyingBuilding != null)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            flyingBuilding.transform.position = new Vector2(worldPosition.x - worldPosition.x % 0.65f-0.4f, worldPosition.y - worldPosition.y % 0.375f + 0.125f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (flyingBuilding)
            {
                flyingBuilding.colaider.enabled = true;
                PlaceFlyingBuilding(flyingBuilding.transform.position.x, flyingBuilding.transform.position.y);
            }
        }

    }


    private void PlaceFlyingBuilding(float placeX, float placeY)
    {
        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }
}

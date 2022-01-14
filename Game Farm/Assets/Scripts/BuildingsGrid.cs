using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid: MonoBehaviour
{
    public Canvas Shop;
    public Vector2Int GridSize = new Vector2Int(10, 10);
    public Player player;
    private Building flyingBuilding;

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (player.LvlRecuired(buildingPrefab.lvl))
        {
            if(player.Transaction(-buildingPrefab.buy_price))
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
        }
        
    }

    private void Update()
    {
        if (!flyingBuilding) return;
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        flyingBuilding.transform.position = new Vector2(worldPosition.x - worldPosition.x % 0.65f-0.4f, worldPosition.y - worldPosition.y % 0.375f + 0.125f);


        if (Input.GetMouseButtonDown(0))
        {

            int s = 0;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(flyingBuilding.transform.position, 0.2f);
            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.tag != "Plot")
                    continue;
                if (col.GetComponentInParent<Field>().isBlocked == false && col.GetComponentInParent<Field>().isPlanted == false)
                    s++;
            }
            if (s == 4)
            {
                Vector3 fpos = new Vector3();

                foreach (Collider2D col in colliders)
                {
                    if (col.gameObject.tag != "Plot")
                        continue;
                    col.GetComponentInParent<Field>().isBlocked = true;
                    col.GetComponentInParent<Field>().Check();
                    fpos += col.transform.position;

                }
                fpos /= 4;
                fpos.x += (float)-0.013;
                fpos.y += (float)+0.6;


                if (flyingBuilding)
                {
                    flyingBuilding.colaider.enabled = true;
                    flyingBuilding.transform.position = fpos;
                    PlaceFlyingBuilding(fpos.x, fpos.y);
                }
            }
         
        }
        if (Input.GetMouseButtonDown(1))
        {
            player.Transaction(flyingBuilding.buy_price);
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

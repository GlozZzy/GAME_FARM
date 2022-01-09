using UnityEngine;

public class CreateButton : MonoBehaviour
{
    public Recipies recipie;
    public ProductionBuilding building;
    public BuildingsMenu buildingsMenu;

    public void SendRecipie()
    {
        building.CreateProduct(recipie);
    }

    public void CloseCanvas()
    {
        buildingsMenu.CloseMenu();
    }
}

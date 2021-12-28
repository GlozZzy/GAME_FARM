using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddock : MonoBehaviour
{
    PaddockMenu paddockMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("Hello");
        OpenMenu();
    }
    public void OpenMenu()
    {
        paddockMenu = FindObjectOfType<PaddockMenu>();
        
        paddockMenu.Open(this);
    }
}

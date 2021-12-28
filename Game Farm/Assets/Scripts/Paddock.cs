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
    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponent<Animals>();   
    }

    // Update is called once per frame
    void Update()
    {
        
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

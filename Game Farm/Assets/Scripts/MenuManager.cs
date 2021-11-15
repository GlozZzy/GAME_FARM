using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Menu;
    public GameObject Picture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void ResumeGame()
    {
        Menu.SetActive(false);
        Picture.SetActive(false);
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        Menu.SetActive(true);
        Picture.SetActive(true);
        GameIsPaused = true;
    }

    public void NewGame()
    {
        print("New Game");
    }

    public void SaveGame()
    {
        print("SaveGame");
    }

    public void Settings()
    {
        print("Settings");
    }

    public void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }
}

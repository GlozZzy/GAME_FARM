using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Menu;
    public GameObject Picture;
    public GameObject PlayerInfo;


    void Start()
    {
        ResumeGame();
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
        PlayerInfo.SetActive(true);
        Picture.SetActive(false);
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        Menu.SetActive(true);
        PlayerInfo.SetActive(false);
        Picture.SetActive(true);
        GameIsPaused = true;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
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

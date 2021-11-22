using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;

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
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        SceneManager.LoadScene(0);
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

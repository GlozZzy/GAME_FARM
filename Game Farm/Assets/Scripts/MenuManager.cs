using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = true;
    public GameObject PlayerInfo;
    public AudioSource song;
    public AudioClip menusong;
    public AudioClip gamesong;

    public GameObject musicoffbttn;
    public GameObject musiconbttn;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if (firstlaunch) GameObject.Find("ContinueBtn").GetComponent<Button>().interactable = false;
        //else GameObject.Find("ContinueBtn").GetComponent<Button>().interactable = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void ResumeGame()
    {
        GetComponent<Canvas>().enabled = false;
        PlayerInfo.SetActive(true);
        GameIsPaused = false;
        song.clip = gamesong;
        song.Play();

    }

    public void PauseGame()
    {
        GetComponent<Canvas>().enabled = true;
        PlayerInfo.SetActive(false);
        GameIsPaused = true;
        song.clip = menusong;
        song.Play();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);

    }

    public void SaveGame()
    {
        print("SaveGame");
    }

    public void MusicOff()
    {
        song.Stop();
        musicoffbttn.SetActive(false);
        musiconbttn.SetActive(true);
    }

    public void MusicOn()
    {
        song.Play();
        musicoffbttn.SetActive(true);
        musiconbttn.SetActive(false);
    }

    public void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }
}

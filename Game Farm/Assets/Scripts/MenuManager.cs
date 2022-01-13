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

    public GameObject resetNotification;
    public GameObject saveNotification;

    public float timeToDisableNotifications;
    private float curTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime < 0) DisableNotifications();

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
        song.volume = 0f;
        musicoffbttn.SetActive(false);
        musiconbttn.SetActive(true);
    }

    public void MusicOn()
    {
        song.volume = 0.003f;
        musicoffbttn.SetActive(true);
        musiconbttn.SetActive(false);
    }

    public void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }

    public void DisableNotifications()
    {
        resetNotification.SetActive(false);
        saveNotification.SetActive(false);
    }

    public void EnableNotifications(GameObject obj)
    {
        obj.SetActive(true);
        curTime = timeToDisableNotifications;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("ALL MENUS")]
    public GameObject pasueMenuUI;
    public GameObject endGameMenu;
    public GameObject objectivesMenu;
    public static bool GameIsStopped = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;

            }
            else{
                Pasuse();
        Cursor.lockState=CursorLockMode.None;

            }
        }
        else if(Input.GetKeyDown("m"))
        {
            if(GameIsStopped)
            {
                RemoveObjectives();
                Cursor.lockState = CursorLockMode.Locked;

            }
            else{
                ShowObjectives();
                Cursor.lockState = CursorLockMode.None;


            }
        }
    }
    public void ShowObjectives()
    {
        objectivesMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;

    }
    public void RemoveObjectives()
    {
        objectivesMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }
    public void Resume()
    {
        pasueMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;

    }
    public void ReStart()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadMenu()
    {
       SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    void Pasuse()
    {
        pasueMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;

    }

}

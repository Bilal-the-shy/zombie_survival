using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject selectCharacter;
    public GameObject mainMenu;


    public void OnselectCharacter()
    {
        selectCharacter.SetActive(true);
        mainMenu.SetActive(false);

    }
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Zombie_Land");
    }
    public void OnQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    


}
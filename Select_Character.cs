using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_Character : MonoBehaviour
{
    public GameObject selectCharacter;
    public GameObject mainMenu;
    public void OnBackButton()
    {
        selectCharacter.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void OnCharacter1()
    {
        SceneManager.LoadScene("Zombie_Land");
    }

    public void OnCharacter2()
    {
        SceneManager.LoadScene("Zombie_Land_2");
    }

    public void OnCharacter3()
    {
        SceneManager.LoadScene("Zombie_Land_3");

    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective_4 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            Objectives.occurence.GetObjectivesDone(true, true, true, true);
            SceneManager.LoadScene("MainMenu");
        }
    }
}

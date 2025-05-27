using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{


    [Header("Objective to complete")]
    public Text objective_1;
    public Text objective_2;
    public Text objective_3;
    public Text objective_4;
    public static Objectives occurence;
    private void Awake()

    {
        occurence = this;
    }
    public void GetObjectivesDone(bool obj_1, bool obj_2, bool obj_3, bool obj_4)
    {
        if (obj_1 == true)
        {
            objective_1.text = "01. Completed";
            objective_1.color = Color.green;

        }
        else
        {
            objective_1.text = "01. Find the Riffle";
            objective_1.color = Color.white;

        }

        if (obj_2 == true)
        {
            objective_2.text = "01. Completed";
            objective_2.color = Color.green;

        }
        else
        {
            objective_2.text = "01. Find Villagers";
            objective_2.color = Color.white;

        }

        if (obj_3 == true)
        {
            objective_3.text = "01. Completed";
            objective_3.color = Color.green;

        }
        else
        {
            objective_3.text = "01. Find Vehicle";
            objective_3.color = Color.white;

        }

        if (obj_4 == true)
        {
            objective_4.text = "01. Completed";
            objective_4.color = Color.green;

        }
        else
        {
            objective_4.text = "01. Get All Villagers to the Vehicle";
            objective_4.color = Color.white;

        }

    }





}

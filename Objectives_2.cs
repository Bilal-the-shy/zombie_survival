using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives_2 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Objectives.occurence.GetObjectivesDone(true, true, false, false);
            Destroy(gameObject,2f);
        }
    }
}


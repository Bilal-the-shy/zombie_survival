using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch")]

    public Camera cam;
    public float giveDamageOf = 10f;
    public float punchRange = 5f;

   // punch effect
//    public GameObject WoodenEffect;


    public void Punch()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie_1_script zombie1 = hitInfo.transform.GetComponent<Zombie_1_script>();
            Zombie_2_Script zombie2 = hitInfo.transform.GetComponent<Zombie_2_Script>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
               
            }
             else if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(giveDamageOf);
              
            }
             else if (zombie2 != null)
            {

                zombie2.ZombieHitDamage(giveDamageOf);
              
            }
        }
    }

}

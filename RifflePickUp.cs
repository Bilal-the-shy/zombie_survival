using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifflePickUp : MonoBehaviour
{
   [Header("Pickup Riffle")]
   public GameObject PlayerRiffle;
   public GameObject PickUpRiffle;
   public PlayerPunch playerPunch;
   public GameObject rifelUI;

   [Header("Riffle assign things")]

   public PlayerMove player;
   private float radius = 2.5f;
   public Animator animator;
   private float nextTimeToPunch=0f;
   public float punchCharge=15f;

   

   public void Awake()
   {
    PlayerRiffle.SetActive(false);
    rifelUI.SetActive(false);
   }
   private void Update()
   {

    if(Input.GetButton("Fire1")&& Time.time>nextTimeToPunch)
    {
        animator.SetBool("Punching", true);
        animator.SetBool("Idle",false);
        nextTimeToPunch=Time.time+1f/punchCharge;
        playerPunch.Punch();
    }
    else{
        animator.SetBool("Punching",false);
        animator.SetBool("Idle",true);
    }
    if (Vector3.Distance(transform.position, player.transform.position)<radius)
    {

        if(Input.GetKeyDown("f"))
        {
            PlayerRiffle.SetActive(true);
            PickUpRiffle.SetActive(false);
            //sound
            //objective completed




            Objectives.occurence.GetObjectivesDone(true,false,false,false);
        }
    }
   }


}

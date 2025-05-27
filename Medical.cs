using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medical : MonoBehaviour
{
  [Header("Health Boost")]
   public PlayerMove player;
  public float healthToGive = 120f;
  public float radius = 2.5f;
  [Header("Sound")]
  public AudioClip HealthBoostSound;
  public AudioSource audioSource;

  [Header("Health Box Animation")]
  public Animator animator;



  public void Update()
  {
    if (Vector3.Distance(transform.position, player.transform.position) < radius)
    {
      if (Input.GetKeyDown("f"))
      {
        animator.SetBool("Open", true);
        player.presentHealth = healthToGive;


        //sound effect
        audioSource.PlayOneShot(HealthBoostSound);
        Object.Destroy(gameObject, 1.5f);
      }
    }
  }
}

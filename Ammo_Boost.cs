using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Boost : MonoBehaviour
{
    [Header("Ammo Boost")]
    public Riffle riffle;
  public int magToGive = 15;
  public float radius = 2.5f;
    [Header("Sound")]
    public AudioClip AmmoBoostSound;
  public AudioSource audioSource;

    [Header("Health Box Animation")]
    public Animator animator;



  public void Update()
  {
    if (Vector3.Distance(transform.position, riffle.transform.position) < radius)
    {
      if (Input.GetKeyDown("f"))
      {
        animator.SetBool("Open", true);
                riffle.mag = magToGive;

        //sound effect
        audioSource.PlayOneShot(AmmoBoostSound);
        Object.Destroy(gameObject, 1.5f);
      }
    }
  }
}

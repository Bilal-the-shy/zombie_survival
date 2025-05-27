using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Riffle : MonoBehaviour
{

    [Header("Riffle stuff")]
    public Animator animator;
    public Camera cam;
    public float giveDamageOf = 10f;

    public float shootingRange = 100f;

    public ParticleSystem muzzleSpark;

    public PlayerMove player;
    public Transform hand;
    public GameObject rifelUI;

    [Header("Riffle Ammunition System")]
    private int maximumAmmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadinTime = 1.3f;
    private bool setReloading = false;

    [Header("Sounds and UI")]
    public GameObject AmmoOutUi;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public  AudioSource audioSource;
    [Header("Riffle Effects")]
    public GameObject goreEffect;
    public GameObject WoodenEffect;
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0f;



    private void Awake()
    {
        transform.SetParent(hand);
        rifelUI.SetActive(true);
        presentAmmunition = maximumAmmunition;
        audioSource= GetComponent<AudioSource>();
    }


    public void Update()
    {
        if (setReloading)
        {
            return;
        }

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }



        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);

        }
    }

    private void Shoot()
    {

        //check for magazine
        if (mag == 0)
        {
            //show amo out text
            StartCoroutine(ShowAmmoOut());
            return;
        }
        presentAmmunition--;
        if (presentAmmunition == 0)
        {
            mag--;
        }
        // updating the player ui
        Ammo_Count.occurrence.UpdateAmmoText(presentAmmunition);
        Ammo_Count.occurrence.UpdateMagText(mag);

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie_1_script zombie1 = hitInfo.transform.GetComponent<Zombie_1_script>();
            Zombie_2_Script zombie2 = hitInfo.transform.GetComponent<Zombie_2_Script>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodenEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
            else if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(giveDamageOf);
                GameObject goreEffectGO = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGO, 1f);
            }
            else if (zombie2 != null)
            {

                zombie2.ZombieHitDamage(giveDamageOf);
                GameObject goreEffectGO = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGO, 1f);
            }
        }
    }




    IEnumerator Reload() // this ieumerator thing is used to stop the execution of the program for a short period of time
    {
        player.Player_Speed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading.....");
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadinTime);
        animator.SetBool("Reloading", false);
        presentAmmunition = maximumAmmunition;
        player.Player_Speed = 1.9f;
        player.playerSprint = 3f;
        setReloading = false;


    }

    IEnumerator ShowAmmoOut()
    {
        AmmoOutUi.SetActive(true);
        yield return new WaitForSeconds(5f);
        AmmoOutUi.SetActive(false);

    }

}

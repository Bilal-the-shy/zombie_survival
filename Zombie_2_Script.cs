using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_2_Script : MonoBehaviour
{

    [Header("Zombie Health and damage")]
    private float zombieHealth = 100f;
    private float zombiePresentHealth;
    public float giveDamage = 5f;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform lookPoint;
    public Camera attackingRaycastArea;
    public LayerMask playerLayer;
    public Transform playerBody;
    [Header("zombie standing ")]

    public float zombieSpeed;

    [Header("zombie attacking variables")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("zombie Moves")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackingRadius;

    // ("Zombie Animations")]

    public Animator anim;

    public void Awake()
    {
        zombiePresentHealth = zombieHealth;

        zombieAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, playerLayer);

        if (!playerInVisionRadius && !playerInAttackingRadius) Idle();
        if (playerInVisionRadius && !playerInAttackingRadius) PersuePlayer();
        if (playerInVisionRadius && playerInAttackingRadius) AttackPlayer();
        

    }
    private void Idle()
    {
        zombieAgent.SetDestination(transform.position);
        anim.SetBool("Idle",true);
        anim.SetBool("Run",false);

    }
    private void PersuePlayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))

        {
            anim.SetBool("Idle",false);
            anim.SetBool("Run",true);

            anim.SetBool("Attack",false);
        }
    }
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(attackingRaycastArea.transform.position, attackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfo.transform.name);
                PlayerMove playerBody = hitInfo.transform.GetComponent<PlayerMove>();

                if (playerBody != null)
                {
                    playerBody.PlayerHitDamage(giveDamage);
                }
                //animations
            anim.SetBool("Attack",true);
            anim.SetBool("Run",false);
            
            }
            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }
    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }
    public void ZombieHitDamage(float takeDamage)
    {
        zombiePresentHealth -= takeDamage;
        if (zombiePresentHealth <= 0)
        {
           
               anim.SetBool("Die", true);
                 ZombieDie();
        }
    }
    private void ZombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInAttackingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 3f);
    }

    internal void ObjectHitDamage(float giveDamageOf)
    {
        throw new System.NotImplementedException();
    }





}

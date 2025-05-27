using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_1_script : MonoBehaviour
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
    [Header("zombie guarding")]
    public GameObject[] walkPoint;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingPointRadius = 2f;
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

        if (!playerInVisionRadius && !playerInAttackingRadius) Guard();
        if (playerInVisionRadius && !playerInAttackingRadius) PersuePlayer();
        if (playerInVisionRadius && playerInAttackingRadius) AttackPlayer();

    }
    private void Guard()
    {


        if (Vector3.Distance(walkPoint[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoint.Length);
            if (currentZombiePosition >= walkPoint.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoint[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        //change zombie facing
        transform.LookAt(walkPoint[currentZombiePosition].transform.position);
    }
    private void PersuePlayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))

        {
            //animations
            anim.SetBool("Walk", false);
            anim.SetBool("Run", true);
            anim.SetBool("Attack", false);
            anim.SetBool("Die", false);


        }
        else{
             //animations
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
            anim.SetBool("Die", true);

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
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Attack", true);
            anim.SetBool("Die", false);


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
             //animations
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
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

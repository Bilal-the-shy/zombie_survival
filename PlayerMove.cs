using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSprint = 3f;
    public float Player_Speed = 1.9f;
    [Header("Player Script Cameras")]
     
    public Transform playerCamera;
    public GameObject endGameMenuUI;
    [Header("PLAYER HEALTH THINGS")]
    public GameObject playerDamage;
    private float PlayerHealth = 120f;
    public float presentHealth;
    public HealthSystem healthSystem;

    [Header("Player Animator and Gravity")]

    public float gravity = -9.8f;
    public Animator animator;

    [Header(" player jumping and velicity")]
    public float turnCalmTime = 0.3f;
    float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    public CharacterController CC;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = PlayerHealth;
        healthSystem.GiveFullHealth(PlayerHealth);
    }

    private void Update()
    {
        Debug.Log("yooooo wassup");

        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        CC.Move(velocity * Time.deltaTime);

        playerMove();

        Jump();

        Sprint();

    }

    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {

            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RiffleWalk", false);
            animator.SetBool("IdleAim", false);






            float targerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targerAngle, ref turnCalmVelocity, turnCalmTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targerAngle, 0f) * Vector3.forward;

            CC.Move(moveDirection.normalized * Player_Speed * Time.deltaTime);
        }
        else
        {

            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jumping");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jumping");
        }
    }

    void Sprint()
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {

            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);

                float targerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targerAngle, ref turnCalmVelocity, turnCalmTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targerAngle, 0f) * Vector3.forward;

                CC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);

            }

        }
    }
    public void PlayerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        StartCoroutine(PlayerDamage());
        healthSystem.SetHealth(presentHealth);

        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }
    private void PlayerDie()
    
    {
        endGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        UnityEngine.Object.Destroy(gameObject, 1.0f);
    }

    IEnumerator PlayerDamage()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(1f);
        playerDamage.SetActive(false);

    }
}
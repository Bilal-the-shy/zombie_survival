using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Vehicle_Controller : MonoBehaviour
{
    [Header("Vehicle Controller")]
    public WheelCollider FRC;
    public WheelCollider FLC;

    public WheelCollider BRC;

    public WheelCollider BLC;
    [Header("wHEEL transforms")]
    public Transform FRT;
    public Transform FLT;

    public Transform BRT;

    public Transform BLT;
    public Transform vehicleDoor;
    [Header("Vehicle Engine")]
    public float accelerationForce = 100f;
    public float presentAcceleration = 0f;
    private float breakeForce = 200f;
    private float presentbreakForce = 0f;

    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehice Security")]
    public PlayerMove player;
    public float radius = 5f;
    private bool isOpened = false;
    [Header("Disable Things")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject PlayerCharacter;
    [Header("Vehicle Hit variables")]
    public float hitRange = 2f;

    private float giveDamageOf = 100f;
    public GameObject goreEffect;
    public GameObject DestroyEffect;
    public Camera cam;





    void Update()
    {


        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpened = true;
                radius = 5000f;
                // objectives complete
                Objectives.occurence.GetObjectivesDone(true, true, true, false);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;
            }
        }


        if (isOpened == true)
        {
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            PlayerCharacter.SetActive(false);

            //Calling Functions
            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
            HitDamage();
        }
        else if (isOpened == false)
        {
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            PlayerCharacter.SetActive(true);
        }






    }

    void MoveVehicle()
    {
        FRC.motorTorque = presentAcceleration;
        FLC.motorTorque = presentAcceleration;
        BLC.motorTorque = presentAcceleration;
        BRC.motorTorque = presentAcceleration;
        presentAcceleration = accelerationForce * -Input.GetAxis("Vertical");
    }

    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        FLC.steerAngle = presentTurnAngle;
        FRC.steerAngle = presentTurnAngle;

        SteeringWheels(FRC, FRT);

        SteeringWheels(FLC, FLT);

        SteeringWheels(BRC, BRT);

        SteeringWheels(BLC, BLT);

    }
    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        WC.GetWorldPose(out Vector3 position, out Quaternion rotation);
        WT.position = position;
        WT.rotation = rotation;
    }
    void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            presentbreakForce = breakeForce;
        }
        else
        {
            {
                presentbreakForce = 0;
            }

        }
        FRC.brakeTorque = presentbreakForce;
        FLC.brakeTorque = presentbreakForce;

        BRC.brakeTorque = presentbreakForce;

        BLC.brakeTorque = presentbreakForce;


    }
    void HitDamage()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, hitRange))
        {
            Debug.Log(hitInfo.transform.name);


            Zombie_1_script zombie1 = hitInfo.transform.GetComponent<Zombie_1_script>();
            Zombie_2_Script zombie2 = hitInfo.transform.GetComponent<Zombie_2_Script>();

            if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(giveDamageOf);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;

                GameObject goreEffectGO = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGO, 1f);
            }
            else if (zombie2 != null)
            {

                zombie2.ZombieHitDamage(giveDamageOf);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;
                ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();

                GameObject goreEffectGO = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGO, 1f);
            }

           
        }
    }
}
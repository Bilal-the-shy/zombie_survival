using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COM_Adjuster : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Center of Mass Adjustment")]
    public Vector3 centerOfMassOffset = new Vector3(0, -0.5f, 0); // Adjust Y as needed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.centerOfMass += centerOfMassOffset;
        }
        else
        {
            Debug.LogError("Rigidbody not found on the vehicle!");
        }
    }
}

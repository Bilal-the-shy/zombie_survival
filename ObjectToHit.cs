using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectToHit : MonoBehaviour
{

    public float ObjectHealth = 30f;

    public void ObjectHitDamage(float amount)
    {
        ObjectHealth -= amount;
        if (ObjectHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }



}

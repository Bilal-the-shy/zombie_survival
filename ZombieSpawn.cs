using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
     [Header("DangerZone Sound")]
     public AudioClip dangerZoneSound;
     public AudioSource audioSource;

    [Header("Zombie spawn variables")]

    public GameObject zombiePrefab;
    public Transform zombieSpawnPosition;
    private float repeatCycle = 1f;
    public GameObject dangerZone_1;



   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            audioSource.PlayOneShot(dangerZoneSound);
            StartCoroutine(DangerZoneTimer());
            Destroy(gameObject, 15f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }

    IEnumerator DangerZoneTimer()
    {
        dangerZone_1.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerZone_1.SetActive(false);

    }

}

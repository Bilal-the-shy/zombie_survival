using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

private AudioSource audioSource;
[Header("Foot step sound")]

public AudioClip[] footstepsSound;
private void Awake()
{
    audioSource=GetComponent<AudioSource>();

}
private AudioClip GetRandomFootStep()
{
    return footstepsSound[UnityEngine.Random.Range(0,footstepsSound.Length)];
}
public void Step()
{
AudioClip clip= GetRandomFootStep();
audioSource.PlayOneShot(clip);   
}

}

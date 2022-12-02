using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioClip glitch;
    public AudioSource sfxPlayer;

    private void OnTriggerEnter(Collider other)
    {
        sfxPlayer.PlayOneShot(glitch);
        Destroy(gameObject);
    }
}

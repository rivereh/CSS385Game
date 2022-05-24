using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneMusic : MonoBehaviour
{   
    public AudioSource audioSource;
    public AudioClip towerMusic;
    public AudioClip safeZoneMusic;

    void Start()
    {
        audioSource.clip = safeZoneMusic;
        audioSource.Play();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip = safeZoneMusic;
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip = towerMusic;
            audioSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBossSound: MonoBehaviour
{
    public AudioSource audioSource;  // Gán AudioSource trong Inspector
    public AudioClip deathSound;      // Gán file âm thanh trong Inspector

    void PlayDeathSound()
    {
        if (audioSource && deathSound && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}

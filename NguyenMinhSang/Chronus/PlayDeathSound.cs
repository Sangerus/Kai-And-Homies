using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBossSound: MonoBehaviour
{
    public AudioSource audioSource;  // G�n AudioSource trong Inspector
    public AudioClip deathSound;      // G�n file �m thanh trong Inspector

    void PlayDeathSound()
    {
        if (audioSource && deathSound && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBossSound: MonoBehaviour
{
    public AudioSource audioSource;  // G�n AudioSource trong Inspector
    public AudioClip hitSound;      // G�n file �m thanh trong Inspector

    void PlayHitSound()
    {
        if (audioSource && hitSound && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}

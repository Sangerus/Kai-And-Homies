using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBossSound: MonoBehaviour
{
    public AudioSource audioSource;  // Gán AudioSource trong Inspector
    public AudioClip hitSound;      // Gán file âm thanh trong Inspector

    void PlayHitSound()
    {
        if (audioSource && hitSound && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}

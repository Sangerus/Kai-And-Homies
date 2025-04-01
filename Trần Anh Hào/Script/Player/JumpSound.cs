using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip jumpSound;   // Drag your sound file here

    public void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            // If the same sound is already playing, stop it first
            if (audioSource.isPlaying && audioSource.clip == jumpSound)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(jumpSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}

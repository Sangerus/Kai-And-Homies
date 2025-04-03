using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    private bool isActive = false;
    private Animator animator;

    public GameObject HiddenDoor;
    public GameObject HiddenDoorOpen;

    [SerializeField] private AudioSource audioSource; // Music System
    [SerializeField] private AudioClip[] musicClips;
    private int currentTrackIndex = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    
        if (HiddenDoor == null)
            HiddenDoor = GameObject.FindWithTag("HiddenDoor");

        if (HiddenDoorOpen == null)
            HiddenDoorOpen = GameObject.FindWithTag("HiddenDoorOpen");

        if (audioSource == null)
            audioSource = FindObjectOfType<AudioSource>(); // Auto-find if not set

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            ActivateLever();
        }
    }

    private void ActivateLever()
    {
        animator.SetTrigger("Hit");
        isActive = true;

        if (HiddenDoor != null) HiddenDoor.SetActive(false);
        if (HiddenDoorOpen != null) HiddenDoorOpen.SetActive(true);
        ChangeMusic();

        // Save state
        DataPersistenceManager.instance.SaveGame();
    }

    private void ChangeMusic()
    {
        if (audioSource != null && musicClips.Length > 0)
        {
            audioSource.Stop();
            currentTrackIndex = (currentTrackIndex + 1) % musicClips.Length;
            audioSource.clip = musicClips[currentTrackIndex];
            audioSource.Play();
            Debug.Log("Music changed to: " + musicClips[currentTrackIndex].name);
        }
        else
        {
            Debug.LogError("No music clips available.");
        }
    }

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        if (data.leverStates.ContainsKey(id))
        {
            isActive = data.leverStates[id];

            if (HiddenDoor == null)
                HiddenDoor = GameObject.FindWithTag("HiddenDoor");

            if (HiddenDoorOpen == null)
                HiddenDoorOpen = GameObject.FindWithTag("HiddenDoorOpen");

            StartCoroutine(DelayedActivation());
        }
    }

    private IEnumerator DelayedActivation()
    {
        yield return new WaitForSeconds(0.1f); // Small delay for scene load

        if (isActive)
        {
            if (HiddenDoor != null) HiddenDoor.SetActive(false);
            if (HiddenDoorOpen != null) HiddenDoorOpen.SetActive(true);
            animator.SetTrigger("Hit");
            ChangeMusic();
        }
    }

    public void SaveData(GameData data)
    {
        if (!data.leverStates.ContainsKey(id))
        {
            data.leverStates.Add(id, isActive);
        }
        else
        {
            data.leverStates[id] = isActive;
        }
    }
}

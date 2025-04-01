using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    private bool isActive = false;
    private Animator animator;

    public GameObject HiddenDoor;
    public GameObject HiddenDoorOpen;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Nếu HiddenDoor chưa được gán, tìm lại theo tên hoặc tag
        if (HiddenDoor == null)
            HiddenDoor = GameObject.FindWithTag("HiddenDoor");

        if (HiddenDoorOpen == null)
            HiddenDoorOpen = GameObject.FindWithTag("HiddenDoorOpen");
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

        // Lưu trạng thái vào hệ thống lưu game
        DataPersistenceManager.instance.SaveGame();
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

            StartCoroutine(DelayedActivation()); // Thêm Coroutine để đảm bảo Animator kịp cập nhật
        }
    }

    private IEnumerator DelayedActivation()
    {
        yield return new WaitForSeconds(0.1f); // Chờ một chút cho scene load hoàn toàn

        if (isActive)
        {
            if (HiddenDoor != null) HiddenDoor.SetActive(false);
            if (HiddenDoorOpen != null) HiddenDoorOpen.SetActive(true);
            animator.SetTrigger("Hit");
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

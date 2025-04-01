using UnityEngine;

public class Respawn : MonoBehaviour, IDataPersistence
{
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void RespawnPlayer()
    {
        transform.position = currentCheckpoint.position - new Vector3(0, 1, 0);
        playerHealth.Respawn();

    }

    public void BackToCheckPoint()
    {
        if (currentCheckpoint == null)
        {
            Debug.LogWarning("No checkpoint set!");
            return;
        }

        transform.position = currentCheckpoint.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CheckPoint"))
        {
            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
            if (checkpoint != null)
            {
                string checkpointID = checkpoint.id; // Lấy ID từ Checkpoint.cs

                // Lưu trạng thái vào game data
                DataPersistenceManager.instance.gameData.activatedCheckpoints[checkpointID] = true;

                // Lưu game ngay khi chạm vào checkpoint
                DataPersistenceManager.instance.SaveGame();

                collision.GetComponent<Collider2D>().enabled = false;
                collision.GetComponent<Animator>().SetTrigger("Hit");

                currentCheckpoint = checkpoint.transform; // Cập nhật Checkpoint hiện tại
            }
        }


        if (collision.CompareTag("Trap"))
        {
            BackToCheckPoint();
        }
    }

    public void LoadData(GameData data)
    {
        if (data.checkpointPosition != Vector3.zero)
        {
            transform.position = data.checkpointPosition;

            currentCheckpoint = new GameObject("Checkpoint").transform;
            currentCheckpoint.position = data.checkpointPosition;
        }

        // Duyệt qua tất cả Checkpoint trong scene và kiểm tra trạng thái đã kích hoạt
        foreach (var checkpoint in FindObjectsOfType<Checkpoint>())
        {
            string checkpointID = checkpoint.id;
            if (data.activatedCheckpoints.ContainsKey(checkpointID) && data.activatedCheckpoints[checkpointID])
            {
                checkpoint.GetComponent<Collider2D>().enabled = false;
                checkpoint.GetComponent<Animator>().SetTrigger("Hit");
            }
        }
    }

    public void SaveData(GameData data)
    {
        if (currentCheckpoint != null)
        {
            data.checkpointPosition = currentCheckpoint.position;
        }
    }

}

using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    private Respawn respawn;
    private Health health;

    private void Awake()
    {
        // Lấy component từ đối tượng Player
        respawn = GameObject.FindGameObjectWithTag("Player").GetComponent<Respawn>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem va chạm với đối tượng Player không
        if (collision.CompareTag("Player"))
        {     
            respawn.BackToCheckPoint();  // Đưa Player về checkpoint
            health.TakeDamage(0.5f);
        }
    }
}

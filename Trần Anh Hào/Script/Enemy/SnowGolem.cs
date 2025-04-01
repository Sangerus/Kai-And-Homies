using UnityEngine;

public class SnowGolem : MonoBehaviour, IDataPersistence
{
    [Header("Enemy Health")]
    public float Health;

    [Header("Movement Settings")]
    public float moveSpeed;
    public float restoreMoveSpeed;

    [Header("Raycast Settings")]
    public float distance;  // Khoảng cách kiểm tra mặt đất
    public Transform checkPoint;  // Điểm kiểm tra mặt đất
    public LayerMask layerMask;  // Layer mặt đất

    [Header("Unique Enemy ID")]
    public string id;

    private bool facingLeft = true;  // Biến kiểm tra hướng của enemy
    private Animator animator;
    private Rigidbody2D rb;

    [ContextMenu("Generate GUID for ID")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Di chuyển qua lại
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);

        // Kiểm tra xem có phải layer ground không, nếu không thì flip
        RaycastHit2D groundCheck = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);

        // Nếu không có mặt đất dưới enemy và enemy đang hướng sang trái, thì flip
        if (groundCheck == false && facingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingLeft = false;
        }
        // Nếu không có mặt đất dưới enemy và enemy đang hướng sang phải, thì flip
        else if (groundCheck == false && !facingLeft)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingLeft = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            moveSpeed = 0f;
            Health -= damage;
            Invoke("RestoreMoveSpeed", 0.5f);

            if (Health <= 0)
            {
                animator.SetTrigger("Die");
                Invoke("DisableEnemy", 0.5f);
            }
        }
    }

    private void RestoreMoveSpeed()
    {
        moveSpeed = restoreMoveSpeed;
    }

    private void DisableEnemy()
    {
        gameObject.SetActive(false);
        // Lưu trạng thái enemy vào game data bằng GUID
        DataPersistenceManager.instance.gameData.deadEnemies[id] = true;
        DataPersistenceManager.instance.SaveGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(1);
        }
    }

    public void LoadData(GameData data)
    {
        if (data.deadEnemies.ContainsKey(id) && data.deadEnemies[id])
        {
            gameObject.SetActive(false); // Vô hiệu hóa nếu enemy đã bị giết trước đó
        }
    }

    public void SaveData(GameData data)
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (checkPoint == null)
        {
            return;
        }

        // Vẽ một đường thẳng để kiểm tra mặt đất dưới enemy
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);
    }
}

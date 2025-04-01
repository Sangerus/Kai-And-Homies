using System.Collections;
using UnityEngine;

public class Guardian : MonoBehaviour, IDataPersistence
{
    public EnemySpawner spawner;

    [Header("Enemy Health")]
    public float Health;

    [Header("Movement Settings")]
    public float moveSpeed;
    public float chaseSpeed;

    [Header("Attack Settings")]
    public float enemyDamage;
    public float attackRange;
    public float detectionRange;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask attackLayer;

    [Header("State Settings")]
    public Transform playerPosition;
    private Animator animator;
    private bool facingLeft = true;
    private bool inRange = false;

    [Header("Raycast Settings")]
    public float distance;
    public Transform checkPoint;
    public LayerMask layerMask;

    [Header("Unique Enemy ID")]
    public string id;

    [ContextMenu("Generate GUID for ID")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if (playerPosition == null)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (playerPosition == null)
        {
            Debug.LogError("Guardian không tìm thấy Player! Hãy gán `playerPosition` trong Inspector hoặc kiểm tra Tag.");
        }
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, playerPosition.position) <= detectionRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (inRange)
        {
            if (playerPosition.position.x > transform.position.x && facingLeft)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingLeft = false;
            }
            else if (playerPosition.position.x < transform.position.x && !facingLeft)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingLeft = true;
            }

            if (Vector2.Distance(transform.position, playerPosition.position) > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, chaseSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
            RaycastHit2D groundCheck = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);

            if (!groundCheck && facingLeft)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingLeft = false;
            }
            else if (!groundCheck && !facingLeft)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingLeft = true;
            }
        }
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (collInfo != null)
        {
            Health playerTakeDamage = collInfo.GetComponent<Health>();
            if (playerTakeDamage != null)
            {
                playerTakeDamage.TakeDamage(enemyDamage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            animator.SetTrigger("Hurt");
            Health -= damage;
            if (Health <= 0)
            {
                animator.SetTrigger("Die");
                Invoke("DisableEnemy", 0.5f);
            }
        }
    }

    private void DisableEnemy()
    {
        if (spawner != null)
        {
            spawner.RemoveEnemy(gameObject); // Gọi hàm RemoveEnemy từ spawner
        }

        gameObject.SetActive(false);
        // Lưu trạng thái enemy vào game data bằng GUID
        DataPersistenceManager.instance.gameData.deadEnemies[id] = true;
        DataPersistenceManager.instance.SaveGame();
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
        if (checkPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);
        Gizmos.color = Color.blue;
        Vector3 gizmoPosition = transform.position;
        Gizmos.DrawWireSphere(gizmoPosition, detectionRange);
        if (attackPoint == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

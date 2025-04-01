using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float iceFriction;

    [Header("Attack")]
    public float playerDamage;
    public float attackRadius;
    public Transform attackPoint;
    public LayerMask attackLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isOnIce;
    private bool isAttack = false;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Di chuyển trái/phải
        float move = Input.GetAxis("Horizontal");

        if (isOnIce) // Nếu đang trên băng
        {
            rb.velocity = new Vector2(rb.velocity.x * iceFriction, rb.velocity.y);
            if (move != 0)
            {
                rb.AddForce(new Vector2(move * walkSpeed, 0), ForceMode2D.Force);
            }
        }
        else // Bình thường thì vẫn di chuyển như cũ
        {
            rb.velocity = new Vector2(move * walkSpeed, rb.velocity.y);
        }

        // Flip nhân vật khi di chuyển
        if (move != 0)
        {
            transform.localScale = new Vector3(move > 0 ? 1f : -1f, 1f, 1f);
        }

        // Jump animation 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("Jump", true);
        }

        // Movement animation
        if (Mathf.Abs(move) != 0f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
                rb.velocity = new Vector2(move * runSpeed, rb.velocity.y);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }



        // Attack animation
        if (Input.GetMouseButtonDown(0) && isAttack == false)
        {
            isAttack = true;
            animator.SetTrigger("Attack");

            Invoke("ResetAttack", 0.35f);
        }

        // Duck animation
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded == true)
        {
            animator.SetBool("Duck", true);
            rb.velocity = Vector2.zero;
        }
        else
        {
            animator.SetBool("Duck", false);
            animator.speed = 1;
        }

    }

    private void ResetAttack()
    {
        isAttack = false;
    }

    public void PauseDuck()
    {
        animator.speed = 0;
    }

    public void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);

        if (collInfo != null)
        {
            SnowGolem snowGolem = collInfo.GetComponent<SnowGolem>();
            if (snowGolem != null)
            {
                snowGolem.TakeDamage(playerDamage);
            }

            EnemyWithNoHurt enemyWithNoHurt = collInfo.GetComponent<EnemyWithNoHurt>();
            if (enemyWithNoHurt != null)
            {
                enemyWithNoHurt.TakeDamage(playerDamage);
            }

            Guardian guardian = collInfo.GetComponent<Guardian>();
            if (guardian != null)
            {
                guardian.TakeDamage(playerDamage);
            }

            HealthBossChecker bossHealth = collInfo.GetComponent<HealthBossChecker>();
            if (bossHealth != null)
            {
                bossHealth.ReduceHealth((int)playerDamage);
            }
        }
    }

    // Hàm Vẽ
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với mặt đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = false;
        }
    }

}
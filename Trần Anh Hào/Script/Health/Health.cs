using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour, IDataPersistence
{
    [Header("Health")]
    public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("IFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numberOfFlash;
    private bool isVulnerable = false;
    private SpriteRenderer spriteRender;


    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (isVulnerable == true)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(Invulnerable());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("Die");
                GetComponent<Player>().enabled = false;
                dead = true;
            }
       
        }

    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);

    }    

    private IEnumerator Invulnerable ()
    {
        isVulnerable = true;
        Physics2D.IgnoreLayerCollision(7, 8, true); // đổi nếu layer đổi 
        for (int i = 0; i < numberOfFlash; i++) 
        {
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false); // đổi nếu layer đổi 
        isVulnerable = false;
    }    

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("Die");
        animator.Play("Idle");
        GetComponent<Player>().enabled = true;
    }

    public void LoadData(GameData data)
    {
        currentHealth = data.health;
        startingHealth = data.startingHealth;
    }

    public void SaveData(GameData data)
    {
        data.health = currentHealth;
        data.startingHealth = startingHealth;
    }

}

using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    public GameObject hpNumberObject; // The GameObject to unhide (could contain the Text)
    public Text bossHPText;           // Reference to the UI Text
    public int maxHP = 10;            // Boss max HP
    private int currentHP;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP; // Set HP at start
        UpdateBossHPUI();
        if (hpNumberObject != null)
        {
            hpNumberObject.SetActive(false); // Initially hide the UI
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // Prevent negative HP
        UpdateBossHPUI();
    }

    void UpdateBossHPUI()
    {
        if (bossHPText != null)
        {
            bossHPText.text = "Boss HP: " + currentHP + "/" + maxHP;
        }
    }

    // Animation Event to unhide the HP UI
    public void EnableBossHPUI()
    {
        if (hpNumberObject != null)
        {
            hpNumberObject.SetActive(true); // Unhide the UI
        }
    }

    // Optional: Hide the UI when needed
    public void DisableBossHPUI()
    {
        if (hpNumberObject != null)
        {
            hpNumberObject.SetActive(false); // Hide the UI
        }
    }
}
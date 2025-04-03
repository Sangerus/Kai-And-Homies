using UnityEngine;

public class BossHPplay : MonoBehaviour
{
    void Startfight()
    {
        BossHPUI display = FindObjectOfType<BossHPUI>();
        if (display != null)
        {
            display.EnableBossHPUI(); 
        }
    }
}
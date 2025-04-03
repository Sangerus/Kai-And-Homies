using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public bool isAngry = false; // Store the state

    public void SetAngry(bool isAllMonsterDead)
    {
        isAngry = isAllMonsterDead; // Correctly update the state

        if (isAngry)
        {
            // Play new animation or change state
            Debug.Log("Boss is now angry!");
        }
        else
        {
            Debug.Log("Boss is calm.");
        }
    }
}

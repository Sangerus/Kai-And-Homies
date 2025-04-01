﻿using UnityEngine;

public class Spring : MonoBehaviour
{
    public float upwardForce;  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, upwardForce);
            }
        }
    }
}

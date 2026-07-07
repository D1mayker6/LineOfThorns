using System;
using Player;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            GravityChange(playerMovement);
        }
    }

    private void GravityChange(PlayerMovement playerMovement)
    {
        
        var rb = playerMovement.GetComponent<Rigidbody2D>();
        rb.gravityScale = -3;
        rb.AddForceY(200,ForceMode2D.Impulse);
        playerMovement.ReverseForcePower();

    }
}

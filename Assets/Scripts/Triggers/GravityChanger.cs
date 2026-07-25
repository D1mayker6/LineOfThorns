using System;
using Enums;
using Player;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    
    [SerializeField] private GravityValue _gravityValue;
    [SerializeField] private float _impulse = 200f;
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
        var intGravityValue = (int)_gravityValue;
        rb.gravityScale = Mathf.Abs(rb.gravityScale) * intGravityValue;
        if(_gravityValue == GravityValue.Down)
            _impulse *= -1f;
        rb.AddForceY(_impulse,ForceMode2D.Impulse);
        playerMovement.ReverseForcePower(intGravityValue);

    }
}

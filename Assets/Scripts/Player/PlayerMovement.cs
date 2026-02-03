using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _translationSpeed;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _forcePower;
    [SerializeField] private float _checkRadius;
    [SerializeField] private Transform _groundCheck;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {   
        if(Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, 1 << LayerMask.NameToLayer("Ground")))
            _rb.AddForce(Vector2.up * _forcePower, ForceMode2D.Impulse);
    }

    private void Move()
    {
        var move = Vector2.right * (_translationSpeed * Time.fixedDeltaTime);
        /*_rb.linearVelocity = Vector2.right * (_translationSpeed);*/
        /*_rb.AddForce(move);*/
        /*_rb.MovePosition(_rb.position + move);*/
        transform.Translate(move);
    }
    
}

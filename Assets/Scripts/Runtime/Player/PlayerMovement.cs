using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Misc")] 
    [SerializeField] private Transform orientation;
    private Rigidbody _rb;
    private const float PlayerHeight = 2f;
    private Vector3 _moveDir;
    private float _hInput, _vInput;
    
    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float accelSpeed = 100;
    
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDrag;
    [SerializeField] private bool _canJump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        
        _canJump = true;
    }
    
    private void Update()
    {
        isGrounded = MovementHelper.GroundCheck(transform, groundLayer, PlayerHeight);
        if(isGrounded)
            _rb.linearDamping = groundDrag;
        else _rb.linearDamping = 0;

        (_hInput, _vInput) = MovementHelper.GetInputs();
        ManageSpeed();

        if (Input.GetKeyDown(jumpKey) && _canJump && isGrounded)
            Jump();
    }

    private void FixedUpdate() => Move();
    private void Move()
    {
        _moveDir = orientation.forward * _vInput + orientation.right * _hInput;

        switch (isGrounded)
        {
            case true:
                _rb.AddForce(_moveDir.normalized * (moveSpeed * accelSpeed), ForceMode.Force);
                break;
            case false:
                _rb.AddForce(_moveDir.normalized * (moveSpeed * accelSpeed * airMultiplier), ForceMode.Force);
                break;
        }
    }

    private void ManageSpeed()
    {
        var flatVel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            var limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {

        _rb.velocity = new Vector3(_moveDir.x, 0, _moveDir.z);
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        _canJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        _canJump = true;
    }
}

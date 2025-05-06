using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float PlayerHeight = 2;
    
    [Header("Misc")] 
    [SerializeField] private Transform orientation;
    public Transform lastCheckPoint;
    
    [Header("Movement")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveMultiplier;
    
    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground Check & Jumping")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool _canJump;
    
    private float _hInput, _vInput;
    private Vector3 _moveDir;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        
        _canJump = true;
    }

    private void Update()
    {
        ControlSpeed();
        (_hInput, _vInput) = PlayerHelper.GetWASDInputs();
        isGrounded = PlayerHelper.GroundCheck(transform, groundLayer, PlayerHeight);

        _rb.linearDamping = isGrounded ? groundDrag : 0;

        if (Input.GetKeyDown(jumpKey) && _canJump && isGrounded)
        {
            _canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
        if(Input.GetKeyDown(KeyCode.E)) Die();
    }

    private void FixedUpdate() => MovePlayer();
    private void MovePlayer()
    {
        _moveDir = orientation.forward * _vInput + orientation.right * _hInput;
        
        switch (isGrounded)
        {
            case true:
                _rb.AddForce(_moveDir.normalized * (moveSpeed * moveMultiplier), ForceMode.Force);
                break;
            case false:
                _rb.AddForce(_moveDir.normalized * (moveSpeed * moveMultiplier * airMultiplier), ForceMode.Force);
                break;
        }
    }

    private void ControlSpeed()
    {
        //limit speed so it doesnt go over moveSpeed value
        var flatVelo = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        if (flatVelo.magnitude > moveSpeed)
        {
            var limitedVelo = flatVelo.normalized * moveSpeed;
            _rb.linearVelocity = new Vector3(limitedVelo.x, _rb.linearVelocity.y, limitedVelo.z);
        }
    }

    private void Jump()
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        _canJump = true;
    }

    private void Die()
    {
        this.transform.position = lastCheckPoint.position;
    }
}

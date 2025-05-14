using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float PlayerHeight = 2;
    public static bool IsDead = false;
    
    [Header("Misc")] 
    [SerializeField] private Transform orientation;
    public PlayerHealth health;
    public Transform lastCheckPoint;
    
    [Header("Movement")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveMultiplier;
    [SerializeField] private float airMultiplier;
    
    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float detectionRadius;
    
    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    
    [Header("Death shit idfk")] 
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject deathUI;
    
    private float _hInput, _vInput;
    private Vector3 _moveDir, _slopeMoveDir;
    private Rigidbody _rb;
    private RaycastHit _slopeHit;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
     
    }

    private void Update()
    {
        if (IsDead) return;

        // input/movement dir
        isGrounded = PlayerHelper.GroundCheck(this.transform, groundLayer, detectionRadius);
        (_hInput, _vInput) = PlayerHelper.GetWASDInputs();
        _moveDir = orientation.forward * _vInput + orientation.right * _hInput;
        
        _slopeMoveDir = Vector3.ProjectOnPlane(_moveDir, _slopeHit.normal);
        
        // drag control
        _rb.linearDamping = isGrounded ? groundDrag : airDrag;

        if (Input.GetKeyDown(jumpKey) && isGrounded)
            Jump();
    }

    private void FixedUpdate()
    {
        if(IsDead) return;
        
        switch (isGrounded)
        {
            case true when !OnSlope():
                _rb.AddForce(_moveDir.normalized * (moveSpeed * moveMultiplier), ForceMode.Acceleration);
                break;
            case true when OnSlope():
                _rb.AddForce(_slopeMoveDir.normalized * (moveSpeed * moveMultiplier), ForceMode.Acceleration);
                break;
            case false:
                _rb.AddForce(_moveDir.normalized * (moveSpeed * moveMultiplier * airMultiplier), ForceMode.Acceleration);
                break;
        }
    }
    
    private void Jump()
    {
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, PlayerHeight * 0.5f + 0.5f))
        {
            //object normal is diagonal (therefore slope)
            return _slopeHit.normal != Vector3.up;
        }
        return false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //player collisions (win, death, coin, enemy hit, etc)
        if (other.gameObject.CompareTag("Kill Zone"))
            Die();
    }
    
    public void Die()
    {
        IsDead = true;
        gameUI.SetActive(false);
        deathUI.SetActive(true);
        
        //unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        IsDead = false;
        gameUI.SetActive(true);
        deathUI.SetActive(false);
        this.transform.position = lastCheckPoint.position;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
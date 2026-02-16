using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform orientation;
    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction jumpAction;

    [Header("Movement settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float inAirControl = 0.6f;
    private Vector3 moveInput;
    private Vector3 moveDirection;
    private bool jumpPressed;

    [Header ("Ground check")]
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector3 groundCheckSize;
    [SerializeField] bool isGrounded;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        GetActions();
    }

    void Update()
    {
        ReadInput();
        CheckGrounded();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJumping();
    }

    void HandleMovement()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x; 
        Vector3 targetVelocity = moveDirection.normalized * moveSpeed;
        
        /*if (!isGrounded)
        {
            targetVelocity *= inAirControl;         // We can use this, if we wan't to reduce the speed while in air
        }*/

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        transform.rotation = orientation.rotation;
        //rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime); // This has been changed to just setting the velocity directly
    }

    void HandleJumping()
    {
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        jumpPressed = false;
    }

    void ReadInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            jumpPressed = true;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics.OverlapBox(groundCheck.position, groundCheckSize, Quaternion.identity, ground).Length > 0;
    }

    void GetActions()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform orientation;
    private Rigidbody rb;
    private bool jumpPressed;
    private InputAction moveAction;
    private InputAction jumpAction;

    [Header("Movement settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float groundDrag;
    private Vector3 moveInput;
    private Vector3 moveDirection;

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
        transform.rotation = orientation.rotation;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        /*if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 5f;
        }*/
        //rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
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
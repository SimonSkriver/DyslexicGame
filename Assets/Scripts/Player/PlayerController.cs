using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform orientation;
    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector3 spawnPos;
    private float halfHeight;

    [Header("Movement settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    private Vector3 moveInput;
    private Vector3 moveDirection;
    private bool jumpPressed;

    [Header ("Ground check")]
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        GetActions();
        spawnPos = transform.position;
        halfHeight = GetComponent<BoxCollider>().size.y / 2f;
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
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        transform.rotation = orientation.rotation;
    }

    void HandleJumping()
    {
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight + 0.2f, ground);
        Debug.DrawRay(transform.position, Vector3.down * halfHeight, Color.red);
    }

    void GetActions()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            rb.Move(spawnPos, Quaternion.identity);
        }
    }
}
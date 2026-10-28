

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float moveInput;
    private bool isGrounded;
    public bool IsGrounded => isGrounded;
   [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private InputActionReference Move;
    [SerializeField] private InputActionReference Jump;

    private float coyoteCounter;
    private float jumpBufferCounter;
    private int jumpsLeft;

    private void Awake()
    {
   rb = GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
       
    }
    private void OnEnable()
    {
        Move.action.Enable();
        Jump.action.Enable();
    }

    private void OnDisable()
    {
        Move.action.Disable();
        Jump.action.Disable();
    }
    private void Update()
    {
        moveInput = Move.action.ReadValue<Vector2>().x;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
       
        if (isGrounded && rb.linearVelocity.y <= 0.1f) //en el piso se recargan el coyotetime y el salto extra
        {
            coyoteCounter = coyoteTime;
            jumpsLeft = maxJumps - 1;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
        //jump buffer recuerda q apretaste salto
        if (Jump.action.WasPressedThisFrame())
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if(jumpBufferCounter > 0f)
        {
            if (coyoteCounter > 0f) //salto desde el piso o recien caido
            {
                DoJump();
                coyoteCounter = 0f;
            }
            else if (jumpsLeft > 0f) //salto en el aire
            {
                DoJump();
                jumpsLeft--;
            }
        }
        if (Jump.action.WasReleasedThisFrame() && rb.linearVelocity.y > 0f)
        
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
           
        

        Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Flip()
    {
        float size = Mathf.Abs(transform.localScale.x);

        if (moveInput > 0.1f)
            transform.localScale = new Vector3(size, transform.localScale.y, 1f);
        else if (moveInput < -0.1f)
            transform.localScale = new Vector3(-size, transform.localScale.y, 1f);
    }
   private void Movement()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        

    }
    private void DoJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpBufferCounter = 0f;
    }
}

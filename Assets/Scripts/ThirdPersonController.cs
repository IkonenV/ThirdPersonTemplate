using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float minMoveSpeed = 1.5f;
    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 8f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1f;
    public float gravity = -20f;
    public float coyoteTime = 2f;
    private float coyoteTimeTimer;
    bool alreadyJumped;
    private float jumpBufferTimer;
    public float jumpBuffer = 0.12f;
    private bool jumpHeld;
    public float addedJumpForce = 0.1f;
    public float jumpForceTime = 1f;
    private float jumpForceTimeTimer;

    [Header("References")]
    public Transform cameraTransform;

    private CharacterController controller;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private float verticalVelocity;
    public LayerMask movingPlatformMask;
    private bool isPlayerOnMovingPlatform;
    public MovingPlatform currentPlatform;
    public LayerMask fallingPlatformMask;
    private bool isPlayerOnFallingPlatform;
    public FallingPlatform currentFallingPlatform;
    Animator animator;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;
    private Vector3 knockbackVelocity;
    private float knockbackTimer;
    bool isTakingKnockback = false;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Knockback();
        PlatformTest();
        FallingPlatformTest();
        HandleMovement();
        holdJump();
        coyoteTimeTimer += Time.deltaTime;
        if (controller.isGrounded)
        {
            coyoteTimeTimer = 0;
            alreadyJumped = false;
            if(jumpBufferTimer > 0)
            {
                Jump();
                jumpBufferTimer = 0;
            }
        }

        jumpBufferTimer -= Time.deltaTime;

    }
    private void Knockback()
    {
        if(knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            isTakingKnockback = true;
        }
        else
        {
            isTakingKnockback = false;
        }
    }
    public void ApplyKnockback(Vector3 direction)
    {
        direction.y = 0.01f;
        direction.Normalize();

        knockbackVelocity = direction * knockbackForce;
        knockbackTimer = knockbackDuration;
    }
    public void PlatformTest()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down,out hit,0.15f, movingPlatformMask))
        {
            if(hit.collider.transform.parent.TryGetComponent<MovingPlatform>(out MovingPlatform platform))
            {
                isPlayerOnMovingPlatform = true;
                currentPlatform = platform;
            }
            else
            {
                isPlayerOnMovingPlatform = false;
                currentPlatform = null;
            }
        }
        else
        {
            isPlayerOnMovingPlatform = false;
            currentPlatform = null;
        }
    }
    public void FallingPlatformTest()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down,out hit,0.15f, fallingPlatformMask))
        {
            if(hit.collider.TryGetComponent<FallingPlatform>(out FallingPlatform platform))
            {
                isPlayerOnFallingPlatform = true;
                currentFallingPlatform = platform;
                Debug.Log("Tippuvalla platformilla");
            }
            else
            {
                isPlayerOnFallingPlatform = false;
                currentFallingPlatform = null;
            }
        }
        else
        {
            isPlayerOnFallingPlatform = false;
            currentFallingPlatform = null;
        }
    }
    
    
    public void holdJump()
    {
        jumpForceTimeTimer += Time.deltaTime;

        if (jumpHeld && jumpForceTimeTimer < jumpForceTime)
        {
            verticalVelocity += addedJumpForce * Time.deltaTime;
        }
    }

    // Called by the Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Called by the Input System
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public bool coyoteTimeTest(bool onGround)
    {
        if (coyoteTimeTimer < coyoteTime && alreadyJumped == false) return true;
        else return false;

    }
    public void Jump()
    {
        animator.SetTrigger("Jump");
        alreadyJumped = true;
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    jumpHeld = true;
            jumpForceTimeTimer = 0;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteTimeTest(true))
        {
            Jump();
        }
        if(context.performed && !controller.isGrounded)
        {
            jumpBufferTimer = jumpBuffer;
        }
        if (context.canceled)
        {
            jumpHeld = false;
        }
    }



    private void HandleMovement()
    {
        float inputMagnitude = moveInput.magnitude;

        if(inputMagnitude > 0.1f)
        {
            animator.SetBool("Moving",true);
        }
        else
        {
            animator.SetBool("Moving",false);
        }
        if (controller.isGrounded)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
        //alla oleva kohta ei käytössä!!
        float currentMoveSpeed;
        if(inputMagnitude < 0.3f)
        {
            currentMoveSpeed = walkSpeed;
        }
        else if (inputMagnitude < 0.7f)
        {
            currentMoveSpeed = runSpeed;
        }
        else
        {
            currentMoveSpeed = sprintSpeed;
        }

        // Keep the player grounded
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // Camera directions
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Ignore vertical camera rotation
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement relative to camera
        Vector3 moveDirection =
            cameraForward * moveInput.y +
            cameraRight * moveInput.x;

        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        // Move player
        Vector3 playerMovement = moveDirection * moveSpeed * inputMagnitude * Time.deltaTime;
        if (isPlayerOnMovingPlatform)
        {
            playerMovement += currentPlatform.platformMovement;
        }
        else if (isPlayerOnFallingPlatform)
        {
            playerMovement += currentFallingPlatform.platformMovement;
        }
        else if (isTakingKnockback)
        {
            playerMovement += knockbackVelocity * Time.deltaTime;
        }
        controller.Move(playerMovement);
        // Rotate player toward movement direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Gravity
        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(
            Vector3.up * verticalVelocity * Time.deltaTime
        );
    }
}
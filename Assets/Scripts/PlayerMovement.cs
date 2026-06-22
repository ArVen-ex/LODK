using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    bool isFacingRight = true;
    public ParticleSystem smokeFX;
    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMove;

    [Header("Jump")]
    public float jumpPower = 10f;
    public int maxJump = 2;
    int jumpRemain;


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float FallSpeedMultiplier = 2f;

    [Header("WallCheck")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.05f, 0.5f);
    public LayerMask wallLayer;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2;
    bool isWallSliding;

    //wall Jumping
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);



    // Update is called once per frame
    void Update()
    { 
        GroundCheck();
        Gravity();
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMove * moveSpeed, rb.linearVelocityY);
            Flip();
        }
    }

    private void Gravity()
    {
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = baseGravity * FallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
   
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpRemain > 0)
        {
            AudioFXManager.Play("Jump");
            if (context.performed)
            {
                //hold for full power
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
                smokeFX.Play();
                jumpRemain--;
            }
            else if (context.canceled)
            {
                //tap for half power 
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);
                smokeFX.Play();
                jumpRemain--;
            }
        }

        //wall Jump
        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;
            smokeFX.Play();

            //forcing a flip 
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); //wall jump = 0.5f -- jump again in 0.1f

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
    private void Flip()
    {
        if (isFacingRight && horizontalMove < 0 || !isFacingRight && horizontalMove > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;

            if (rb.linearVelocityY == 0)
            {
                smokeFX.Play();
            }
            
        }
    }

    private void WallSlide()
    {
        if (!isGrounded && WallCheck() && horizontalMove != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -wallSlideSpeed));
        }
        else {
            isWallSliding = false;
        
        }

    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = true;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;
            CancelInvoke(nameof(CancelWallJump));
        }else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;

        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpRemain = maxJump;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

   
}

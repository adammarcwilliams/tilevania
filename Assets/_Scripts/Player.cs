using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed = 5;
    [SerializeField] float climbSpeed = 5;
    [SerializeField] float deathKick = 25;

    float initialGravityScale;

    bool isAlive = true;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        initialGravityScale = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        Jump();
        Climb();
        Die();
    }

    void Run()
    {
        // Move the player horizontally
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        // Update animation state & sprite direction
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

        if (playerHasHorizontalSpeed)
        {
            FlipSpriteDirection();
        }
    }

    void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    void Climb()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = initialGravityScale;
            return;
        }

        myRigidbody.gravityScale = 0;

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");

            if (IsFacingRight())
            {
                myRigidbody.velocity = new Vector2(-deathKick, deathKick);
            }
            else
            {
                myRigidbody.velocity = new Vector2(deathKick, deathKick);
            }

        }
    }

    bool IsFacingRight()
    {
        return this.transform.localScale.x > 0;
    }

    void FlipSpriteDirection()
    {
        this.transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1);
    }
}

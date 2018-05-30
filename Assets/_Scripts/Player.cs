using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed = 5;

    bool isAlive = true;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
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
        if (CrossPlatformInputManager.GetButtonDown("Jump") && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    void FlipSpriteDirection()
    {
        this.transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1);
    }
}

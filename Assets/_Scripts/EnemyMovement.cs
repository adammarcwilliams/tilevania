using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 1;

    Rigidbody2D myRigidbody;
    BoxCollider2D probeCollider;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        Walk();
	}

    void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprite();
    }

    void Walk()
    {
        if (IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0);
        }

    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1);   
    }

    bool IsFacingRight()
    {
        return this.transform.localScale.x > 0;
    }
}

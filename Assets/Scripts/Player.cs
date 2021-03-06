﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Config
    public float runSpeed = 5f;
    public float jumpSpeed = 5f;
    public float secondJumpSpeedReducer = 4f;
    public float climbSpeed = 5f;
    public Vector2 deathKick = new Vector2(25f, 25f);

    //State
    bool isAlive = true;
    bool doubleJumpAllowed = false;

    //Cached component references
    Rigidbody2D myRigidbody = default;
    public Animator myAnimator = default;
    CapsuleCollider2D myBodyCollider2D = default;
    BoxCollider2D myFeetCollider2D = default;
    float gravityScaleAtStart = default;

    //Messages then methods

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 do 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) && doubleJumpAllowed)
            {
                Vector2 secondJumpVelocityToAdd = new Vector2(0f, jumpSpeed - secondJumpSpeedReducer);
                myRigidbody.velocity += secondJumpVelocityToAdd;
                myAnimator.SetTrigger("IsJumping");
                doubleJumpAllowed = false;
            }
            else if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            else
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidbody.velocity += jumpVelocityToAdd;
                myAnimator.SetTrigger("IsJumping");
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    doubleJumpAllowed = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallingMovingPlatform"))
        {
            transform.parent = collision.gameObject.transform.GetChild(0).transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.gravityScale = 0f;
        
          bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
          myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void FlipSprite()
    {
        //if player is moving horizontal
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; // ako mi je kretanje po x veće od 0 Mathf.Epsilon je skoro 0
        if (playerHasHorizontalSpeed)
        {
            //revert scaling of axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) || myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
           GetComponent<Rigidbody2D>().velocity = new Vector2 (-Mathf.Sign(myRigidbody.velocity.x) * deathKick.x, deathKick.y);
           isAlive = false;
           myAnimator.SetTrigger("Dying");
            StartCoroutine(ProcessDeathToSession());
        }
    }

    IEnumerator ProcessDeathToSession()
    {
           yield return new WaitForSeconds(1);
           FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}

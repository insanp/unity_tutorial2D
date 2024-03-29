﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float maxSpeedX = 5f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float maxJumpSpeed = 15f;
    [SerializeField] int numJump = 0;
    [SerializeField] int maxNumJump = 1;
    [SerializeField] float stopVelocitySmooth = 200f;

    // for jumping detection
    [SerializeField] Collider2D groundCollider;
    [SerializeField] LayerMask groundLayer;

    public Vector3 respawnPoint;

    private float movement = 0f;

    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    private Animator playerAnimation;
    private Vector2 stopVelocity;
    private LevelManager gameLevelManager;

    private bool isTouchingGround;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = playerAnimation.transform.position;
        gameLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.IsTouchingLayers(groundCollider, groundLayer);
        movement = Input.GetAxis("Horizontal");

        if (movement < 0f || movement > 0f)
        {
            if ((movement < 0f && isFacingRight) || (movement > 0f && !isFacingRight))
            {
                Flip();
            }

            float velocityX = Mathf.Clamp(rigidBody.velocity.x + movement * speed, -maxSpeedX, maxSpeedX);
            rigidBody.velocity = new Vector2(velocityX, rigidBody.velocity.y);
        } else
        {
            // idle
            //stopVelocity = new Vector2(0, rigidBody.velocity.y);
            //rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, stopVelocity, stopVelocitySmooth * Time.deltaTime);
        }

        

        if (Input.GetButtonDown("Jump") && numJump < maxNumJump )
        {
            float newJumpSpeed = Mathf.Clamp(rigidBody.velocity.y + jumpSpeed, jumpSpeed * 0.7f, maxJumpSpeed);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, newJumpSpeed);
        } else if (Input.GetButtonUp("Jump") && !isTouchingGround && numJump < maxNumJump)
        {
            numJump++;

            // stop going up
            if (rigidBody.velocity.y > 0f)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            }
            
        }

        if (isTouchingGround)
        {
            numJump = 0;
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool GetFacingRight()
    {
        return isFacingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            // respawn or dead
            gameLevelManager.Respawn();
        }

        if (collision.tag == "Checkpoint")
        {
            // update respawn point
            var checkpoint = collision.gameObject.GetComponent<CheckpointController>();
            if (!checkpoint.checkpointReached)
            {
                checkpoint.activateCheckpoint();
                respawnPoint = checkpoint.transform.position;
            }
        }
    }
}

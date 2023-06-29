using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Properties")]
    public float moveSpeed;
    public float jumpPower;
    public float jumpTime;
    [SerializeField] float jumpMultiplier;
    [SerializeField] float fallMultiplier;
    public float hangTime;
    private float hangCounter;

    bool isJumping;
    float jumpCounter;

    Vector2 VectorGravity;

    [Space]
    
    [Header("Checking What Direction is The Player Move")]
    [SerializeField] float Horizontal_Move;

    [Space]

    [Header("Ground Systems")]
    [SerializeField] Transform GroundCheck_Point;
    [SerializeField] float GroundCheck_Range;
    [SerializeField] LayerMask WhatIsForGround;

    [Space]

    [Header("Facing System")]
    bool IsFacingRight = true;

    [Space]

    [Header("Components")]
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator animator;

    [Header("Particles")]
    public ParticleSystem FootStep_Effect_Particle;
    public ParticleSystem Impact_Effect_Particle;
    private ParticleSystem.EmissionModule FootStep_Effect_Emission;

    bool WasOnGround;

    void Start()
    {
        VectorGravity = new Vector2(0, -Physics2D.gravity.y);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        FootStep_Effect_Emission = FootStep_Effect_Particle.emission;
    }    

    void Update()
    {
        Horizontal_Move = Input.GetAxis("Horizontal");
        animator.SetFloat("Horizontal_Move", Mathf.Abs(Horizontal_Move));

        // Jumping System
        if (Ground_Check())
        {
            hangCounter = hangTime;
        } 
        else
        {
            hangCounter -= Time.deltaTime;
        }

        Player_Jump_Method();
        JumpAnimationEvent();
    }

    void FixedUpdate()
    {
        Player_Move_Method();

        // Facing Rgiht or Left Checking
        if (IsFacingRight == true && Horizontal_Move < 0)
        {
            IsFacingRight = true;
            Player_Facing_Method();
        }
        else if (IsFacingRight == false && Horizontal_Move > 0)
        {
            IsFacingRight = false;
            Player_Facing_Method();
        }
    }

    private void Player_Move_Method()
    {
        rb2d.velocity = new Vector2(Horizontal_Move * moveSpeed * Time.deltaTime, rb2d.velocity.y);

        // Show Footstep Particle
        if (Horizontal_Move != 0 && Ground_Check()) {
            FootStep_Effect_Emission.rateOverTime = 35f;
        } else {
            FootStep_Effect_Emission.rateOverTime = 0f;
        }

        // Show Impact Particle
        if(!WasOnGround && Ground_Check())
        {
            Impact_Effect_Particle.gameObject.SetActive(true);
            Impact_Effect_Particle.Stop();
            Impact_Effect_Particle.transform.position = FootStep_Effect_Particle.transform.position;
            Impact_Effect_Particle.Play();

            if (!WasOnGround)
            {
                //play jump sound
                FindAnyObjectByType<AudioManager>().Play("Player HitGround Sound");
            }
        }

        WasOnGround = Ground_Check();
    }

    private void Player_Jump_Method()
    {
        if (Input.GetButtonDown("Jump") && hangCounter > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
            isJumping = true;
            jumpCounter = 0;
            //play ground hit sound 
            FindAnyObjectByType<AudioManager>().Play("Player Jump Sound");
        }

        if (rb2d.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if(jumpCounter>jumpTime) isJumping = false;

            float t =jumpCounter / jumpTime;
            float currentJumpmultiplier = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpmultiplier = jumpMultiplier * (1 - t);
            }

            rb2d.velocity += VectorGravity * currentJumpmultiplier * Time.deltaTime;
        }

        if (rb2d.velocity.y > 0)
        {
            rb2d.velocity -= VectorGravity * fallMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.6f);
            }

        }
    }

    private bool Ground_Check()
    {
        bool IsStayOnGround = Physics2D.OverlapCircle(GroundCheck_Point.position, GroundCheck_Range, WhatIsForGround);
        return IsStayOnGround;
    }

    private void Player_Facing_Method()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void JumpAnimationEvent()
    {
        if (rb2d.velocity.y > 0)
        {
            animator.SetBool("IsJump", true);
            animator.SetBool("IsFall", false);
        }
        
        if (rb2d.velocity.y <= 0)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", true);
        }
        
        if (Ground_Check())
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", false);
        }
    }
}

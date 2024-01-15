using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    public float Speed;
    public float JumpForce;
    private Vector2 moveInput;

    private Rigidbody2D rigidbody;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGrounded;

    private bool facingRight = true;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!onLadder && !onFireLadder)
        {
            if(!isDialogueActive && !Final.IsFinalGame() && !Final.IsTheFinalPlayerAnimation())
                moveInput.x = Input.GetAxis("Horizontal");
            rigidbody.velocity = new Vector2(moveInput.x * Speed, rigidbody.velocity.y);
        }
    }

    private void Update()
    {
        DialogueCheck();
        CheckingGround();
        FlipPlayer();
        Jump();
        isShiftDown();
        CheckingLadder();
        LadderMechanics();
        LadderUpDown();
        Ladders();
    }

    bool isDialogueActive;

    void DialogueCheck()
    {
        if (dialogue.activeInHierarchy)
        {
            isDialogueActive = true;            
            moveInput = Vector2.zero;
        }
        else
        {
            isDialogueActive = false;
        }
    }

    void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGrounded);
    }

    void Jump()
    {
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && !isDialogueActive && !Final.IsFinalGame() && !Final.IsTheFinalPlayerAnimation())
        {
            rigidbody.velocity = Vector2.up * JumpForce;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void FlipPlayer()
    {
        if (facingRight == false && moveInput.x > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput.x < 0)
        {
            Flip();
        }

        if (moveInput.x == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    [SerializeField] GameObject text;

    bool shifDown = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            text.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {
            if (shifDown)
            {
                animator.SetBool("isPushing", true);
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                text.SetActive(false);
            }
            else
            {
                animator.SetBool("isPushing", false);
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                if(feetPos.position.y < collision.gameObject.transform.position.y)
                    text.SetActive(true);
            }
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            animator.SetBool("isPushing", false);
            if (text.activeInHierarchy)
                text.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }

    void isShiftDown()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            shifDown = true;
        else
            shifDown = false;
    }

    bool isLadder;
    public LayerMask WhatIsLadder;
    public Transform bottomLadder;
    bool isBottomLadder;
    public LayerMask WhatIsBottomStopper;
    bool isBottomLadderStopper;
    public LayerMask WhatIsTopStopper;
    bool isTopLadderStopper;

    void CheckingLadder()
    {
        isLadder = Physics2D.OverlapPoint(feetPos.position, WhatIsLadder);
        isBottomLadder = Physics2D.OverlapPoint(bottomLadder.position, WhatIsLadder);
        isBottomLadderStopper = Physics2D.OverlapPoint(bottomLadder.position, WhatIsBottomStopper);
        isTopLadderStopper = Physics2D.OverlapPoint(bottomLadder.position, WhatIsTopStopper);
    }

    public float LadderSpeed;

    void LadderMechanics()
    {
        if (onLadder)
        {
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            moveInput.x = moveInput.y;
            if (isBottomLadderStopper || isTopLadderStopper)
                rigidbody.velocity = Vector2.zero;
            if (((isBottomLadderStopper && moveInput.y > 0) || !isBottomLadderStopper) && !isTopLadderStopper)
                rigidbody.velocity = moveInput * LadderSpeed;
            if (((isTopLadderStopper && moveInput.y < 0) || !isTopLadderStopper) && !isBottomLadderStopper)
                rigidbody.velocity = moveInput * LadderSpeed;
        }
        else if (onFireLadder)
        {
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            if (isBottomLadderStopper || isTopLadderStopper)
                rigidbody.velocity = Vector2.zero;
            if (((isBottomLadderStopper && moveInput.y > 0) || !isBottomLadderStopper) && !isTopLadderStopper)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, moveInput.y * LadderSpeed);
            if (((isTopLadderStopper && moveInput.y < 0) || !isTopLadderStopper) && !isBottomLadderStopper)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, moveInput.y * LadderSpeed);
        }
        else
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void LadderUpDown()
    {
        if (!isDialogueActive)
            moveInput.y = Input.GetAxis("Vertical");
        animator.SetFloat("moveY", moveInput.y);
    }


    bool onLadder;
    float verInput;
    void Ladders()
    {
        verInput = Input.GetAxis("Vertical");

        if (isLadder || isBottomLadder)
        {
            if (!isLadder && isBottomLadder)
            {
                if (verInput > 0)
                {
                    onLadder = false;
                }
                else if (verInput < 0)
                {
                    onLadder = true;
                }
            }
            else if (isLadder && isBottomLadder)
            {
                if (verInput > 0)
                {
                    onLadder = true;
                }
                else if (verInput < 0)
                {
                    onLadder = true;
                }
            }
            else if (isLadder && !isBottomLadder)
            {
                if (verInput > 0)
                {
                    onLadder = true;
                }
                else if (verInput < 0)
                {
                    onLadder = false;
                }
            }
        }
        else
        {
            onLadder = false;
        }

        onLadder = onLadder && !LadderHint.IsLadderBlocked();

        LadderMechanics();
    }

    bool onFireLadder = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireLadder"))
        {
            onFireLadder = true;
            animator.SetBool("onFireLadder", onFireLadder);
            rigidbody.velocity = Vector3.zero;
            var scaler = transform.localScale;
            scaler.x = 1;
            transform.localScale = scaler;
            moveInput.x = 0;
            transform.position = new Vector3(collision.GetComponent<BoxCollider2D>().bounds.center.x, transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FireLadder"))
        {
            onFireLadder = false;
            animator.SetBool("onFireLadder", onFireLadder);
            var scaler = transform.localScale;
            scaler.x = -1;
            transform.localScale = scaler;
        }
    }
}

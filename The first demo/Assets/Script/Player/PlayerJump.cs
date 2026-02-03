using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;
    public AudioSource JumpSound;

    [SerializeField] private float JumpSpeed;
    [SerializeField] private float IsGroundCheck;
    [SerializeField] LayerMask GroundLayer;
    private bool IsJump;
    public bool IsGround;
    private bool doubleJump;
    public bool walljumping;

    //跳跃优化，使得可以通过空格按键的长短来实现跳跃高度的改变
    private bool IsJumping;
    [SerializeField] private float JumpAddTime;
    [SerializeField ]private float JumAddTimeController;
    [SerializeField] private float JumpPower;
    [SerializeField] private float FallPower;
    [SerializeField] private float WallJumpSpeed;

    private PlayerWallJump wc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        JumpSound = GetComponent<AudioSource>();
        wc = GetComponentInChildren<PlayerWallJump>();
    }

    void Update()
    {
        Jump();

        WallJump();

        AnimationController();

        GroundCheck();


    }

 

    private void GroundCheck()
    {
        //IsGround
        IsGround = Physics2D.Raycast(transform.position, Vector2.down, 1.05f, GroundLayer);
        
    }

    IEnumerator WallJumping()
    {
        walljumping = true;
        yield return new WaitForSeconds(0.5f);
        walljumping = false;
    }

    private void WallJump()
    {
        if (wc.inWall)
        {
            doubleJump = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
            if (Input.GetButtonDown("Jump")&&!IsGround)
            {
                rb.velocity = new Vector2(-WallJumpSpeed*transform.localScale.x, WallJumpSpeed);
                StartCoroutine(WallJumping());
            }
        }
    }
    private void AnimationController()
    {
        anim.SetBool("isJump", IsJump);
        anim.SetBool("IsGround", IsGround);
        float facenum = Input.GetAxisRaw("Horizontal");
        //face
        if (facenum != 0)
        {
            transform.localScale = new Vector3(facenum, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Jump()
    {
        if (rb.velocity.y > 0.3f) IsJump = true;
        else IsJump = false;

        if (Input.GetButtonDown("Jump") && IsGround)
        {
            JumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
            IsJumping = true;
            JumAddTimeController = 0;
            doubleJump =true;
        }

        //二段跳
        if(doubleJump && !IsGround && Input.GetButtonDown("Jump"))
        {
            JumpSound.Play();
            anim.SetTrigger("IsDoubleJump");
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
            IsJumping = true;
            JumAddTimeController = 0;
            doubleJump = false;
        }

        if (Input.GetButtonUp("Jump"))
        {
            IsJumping = false;
        }
        if (IsJumping && JumAddTimeController < JumpAddTime )
        {   
            rb.velocity += new Vector2(0, -Physics2D.gravity.y * Time.deltaTime * FallPower);
            JumAddTimeController += Time.deltaTime;
        }

        if (!IsJumping)
        {
            rb.velocity -= new Vector2(0 ,-Physics2D.gravity.y * Time.deltaTime*JumpPower);
        }
        //Jump
  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - IsGroundCheck));
    }
}

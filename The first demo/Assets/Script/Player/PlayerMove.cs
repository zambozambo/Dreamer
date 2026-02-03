using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public float MoveSpeed;
    public float MoveControler;
    private bool IsRun;

    public AudioSource runSound;
    private PlayerJump playerJump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (!playerJump.walljumping)  
        {
            XMove();

            AnimationController();
        }
  
    }

    private void AnimationController()
    {
        IsRun = MoveControler != 0;
        anim.SetBool("IsRun", IsRun);
    }

    private void XMove()
    {
        MoveControler = Input.GetAxisRaw("Horizontal");
        if (MoveControler != 0 && playerJump.IsGround)
        {
            if (!runSound.isPlaying)
            {
                runSound.Play();
            }
        }
        else
        {
            runSound.Stop();
        }
        rb.velocity = new Vector2(MoveControler * MoveSpeed, rb.velocity.y);
    }
}

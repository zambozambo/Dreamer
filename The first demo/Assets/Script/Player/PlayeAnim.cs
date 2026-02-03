using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个是没有用的代码，暂时还没有研究透彻，暂时先放一放
public class PlayeAnim : MonoBehaviour
{
    private enum Anim {idle, run, jump, fall };
    private Anim state;
    private Animator anim;
    private PlayerMove playerMove;
    private PlayerJump playerJump;
    public AudioSource JumpSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        JumpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.MoveControler != 0)
        {
            state = Anim.run;
        }else
        {
            state = Anim.idle;
        }

        if(playerJump.rb.velocity.y > 0.3f)
        {
            state = Anim.jump;
        }
        if(playerJump.rb.velocity.y < 0.3f)
        {
            state = Anim.fall;
        }

        anim.SetInteger("States", (int)state);
    }
}

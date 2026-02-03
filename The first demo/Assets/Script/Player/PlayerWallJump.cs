using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    public bool inWall;
    private Animator anim;
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        anim.SetBool("InWall", inWall);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            inWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            inWall = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public Vector2 pos;
    private Animator anim;
    private Rigidbody2D rb;

    private bool isDroped;
    void Start()
    {
        //记得获取组件
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
        isDroped = false;
    }

    void Update()
    {
        if (rb.transform.position.y < -15.0f)
        {
            isDroped = true; 
            anim.SetBool("IsDroped",isDroped);
            rb.bodyType = RigidbodyType2D.Static;
            Revive1();
            Time.timeScale = 1.0f;
            Revive2();
            isDroped = false;
            anim.SetBool("IsDroped", isDroped);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Traps"))
        {
            anim.SetTrigger("IsDead");
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void Revive1()
    {
        transform.position = pos;
    }

    public void Revive2()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}

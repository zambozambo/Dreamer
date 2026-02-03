using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDie pd=collision.GetComponent<PlayerDie>();
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Check");
            pd.pos = transform.position;
        }
    }
}

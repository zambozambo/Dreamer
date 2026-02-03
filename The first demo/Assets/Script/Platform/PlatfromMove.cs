using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromMove : MonoBehaviour
{
    public Transform Platform;
    public Transform Pos1, Pos2;
    private Transform movePos;
    private PlayerMove pm;
    [SerializeField] private float moveSpeed;

    void Start()
    {
            
    }


    void Update()
    {
        if(Platform.position == Pos1.position)
        {
            movePos = Pos2;
        }else if(Platform.position == Pos2.position)
        {
            movePos = Pos1;
        }
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pm.MoveControler == 0)
        {
            pm.rb.transform.position = Vector2.MoveTowards(transform.position, movePos.position, moveSpeed * Time.deltaTime);
        }
    }
}

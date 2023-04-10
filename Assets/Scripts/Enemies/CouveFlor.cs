using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouveFlor : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speedd = 5f;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;
    private Rigidbody2D rb;

    private bool facingLeft = true;
    
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(facingLeft)
        {
            if(transform.position.x > leftCap)
            {
                //Verificar se a sprite está virada para o lado correto, senão virar para o lado correto
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (!coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-speedd, rb.velocity.y);
                }
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
           if(transform.position.x < rightCap)
           {
                //Verificar se a sprite está virada para o lado correto, senão virar para o lado correto
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (!coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(speedd, rb.velocity.y);
                }
            }
            else
            {
                facingLeft = true;
            } 
        }
    }
}

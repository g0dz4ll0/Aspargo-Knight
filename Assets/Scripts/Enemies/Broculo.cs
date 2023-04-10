using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broculo : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
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
        //Passar do salto para queda
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //Passar do salto para idle
        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if(facingLeft)
        {
            if(transform.position.x > leftCap)
            {
                //Verificar se a sprite está virada para o lado correto, senão virar para o lado correto
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                //Testar para ver se o broculo está no chão, e se estiver, pode saltar
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
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
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //Testar para ver se o broculo está no chão, e se estiver, pode saltar
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            } 
        }
    }
}
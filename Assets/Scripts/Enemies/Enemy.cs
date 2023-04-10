using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource death;
    [SerializeField] protected GameObject tomato;
    [SerializeField] protected GameObject boots;
    [SerializeField] protected GameObject leek;
    [SerializeField] protected float dropRate = 1f / 2f;
    [SerializeField] protected float dropRate1 = 1f / 3f;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        death = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        death.Play();
        anim.SetTrigger("Death");
    }

    private void Death()
    {
        Destroy(this.gameObject);
        Instantiate(leek, transform.position + (new Vector3(3,0,0)), Quaternion.identity);
        if (Random.Range(0f, 1f) <= dropRate1)
        {
            Instantiate(boots, transform.position, Quaternion.identity);
        }
        else if (Random.Range(0f, 1f) <= dropRate)
        {
            Instantiate(tomato, transform.position, Quaternion.identity);
        }
    }
}

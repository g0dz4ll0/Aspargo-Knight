using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bering : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    [SerializeField] private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player");
        {
            anim.SetTrigger("Boom");
        }
    }

    private void isBooming()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
